using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [SerializeField]
    private Light directionalLight;
    [SerializeField]
    private LightingPreset preset;
    [SerializeField]
    private float timeOfDay;

    [SerializeField]
    private float timeOfCycleInSecond = 60;

    private static LightingManager instance;

    public Action _isDay, _isNight;
    private bool bIsDay, bIsNight;

    public static LightingManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<LightingManager>();

            return instance;
        }
    }

    private void Start()
    {
        if (timeOfDay >= timeOfCycleInSecond / 4 && timeOfDay <= timeOfCycleInSecond / 4 * 3)
        {
            bIsDay = true;
            bIsNight = false;
        }
        else if (timeOfDay <= timeOfCycleInSecond / 4 || timeOfDay >= timeOfCycleInSecond / 4 * 3)
        {
            bIsDay = false;
            bIsNight = true;
        }
    }

    private void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime;
            timeOfDay %= timeOfCycleInSecond; //Clamb betweek 0-24
            UpdateLighting(timeOfDay/ timeOfCycleInSecond);
            IsDayTime();
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }

    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.ForColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);

            directionalLight.transform.localRotation = Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
        }

    }

    //Try to find a directional light to use if we haven't set one
    private void OnValidate()
    {
        if (directionalLight != null)
            return;

        if (RenderSettings.sun != null)
        {
            directionalLight = RenderSettings.sun;
        }
        else
        {
            Light[] lights = GameObject.FindObjectsOfType<Light>();
            foreach (Light light in lights)
            {
                if (light.type == LightType.Directional)
                {
                    directionalLight = light;
                    return;
                }
            }
        }
    }

    private void IsDayTime()
    {
        if (bIsDay)
        {
            if (timeOfDay <= timeOfCycleInSecond / 4 * 3)
            {
                if (timeOfDay >= timeOfCycleInSecond / 4)
                {
                    _isDay.Invoke();
                    bIsDay = false;
                    bIsNight = true;
                }
            }
        }
        else if(bIsNight)
        {
            if(timeOfDay <= timeOfCycleInSecond / 4 || timeOfDay >= timeOfCycleInSecond / 4 * 3)
            {
                _isNight.Invoke();
                bIsDay = true;
                bIsNight = false;
            }
        }
    }

    public void SetTime(float time)
    {
        timeOfDay = time;
    }
}
