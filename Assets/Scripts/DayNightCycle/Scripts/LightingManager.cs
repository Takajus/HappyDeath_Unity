using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


//TODO : Rename to DayCycleManager
[ExecuteAlways]
public class LightingManager : MonoBehaviour
{
    [Header("References")] [SerializeField]
    private Light directionalLight;

    [SerializeField] private LightingPreset preset;

    [Header("Variables")] [SerializeField] private float timeOfDay;

    [SerializeField] private float timeOfCycleInSecond = 60;

    private static LightingManager instance;

    //TODO: Use enum to know if day or night
    public enum DayCycleState
    {
        Day = 0,
        Night,
    }

    private DayCycleState _cycleState = DayCycleState.Day;

    [SerializeField, Range(0f, 100f)] private float timeMultiplicator = 1f;

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
            _cycleState = DayCycleState.Day;
        }
        else if (timeOfDay <= timeOfCycleInSecond / 4 || timeOfDay >= timeOfCycleInSecond / 4 * 3)
        {
            _cycleState = DayCycleState.Night;
        }
    }

    private void Update()
    {
        if (preset == null)
            return;

        if (Application.isPlaying)
        {
            timeOfDay += Time.deltaTime * timeMultiplicator;
            timeOfDay %= timeOfCycleInSecond; //Clamb betweek 0-24
            UpdateLighting(timeOfDay / timeOfCycleInSecond);
            CheckCycleState();
        }
        else
        {
            UpdateLighting(timeOfDay / 24f);
        }

#if UNITY_EDITOR
        if(InputManager.Instance.editorMultiplicatorValue.action.triggered)
            Multiplicator();
#endif
    }

    private void UpdateLighting(float timePercent)
    {
        RenderSettings.ambientLight = preset.AmbientColor.Evaluate(timePercent);
        RenderSettings.fogColor = preset.ForColor.Evaluate(timePercent);

        if (directionalLight != null)
        {
            directionalLight.color = preset.DirectionalColor.Evaluate(timePercent);

            directionalLight.transform.localRotation =
                Quaternion.Euler(new Vector3((timePercent * 360f) - 90f, 170f, 0));
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

    private void CheckCycleState()
    {
        switch (_cycleState)
        {
            case DayCycleState.Day:
                if (timeOfDay <= timeOfCycleInSecond / 4 * 3)
                {
                    if (timeOfDay >= timeOfCycleInSecond / 4)
                    {
                        _cycleState = DayCycleState.Night;
                        StartCoroutine(_CoroutineDayBegin());
                    }
                }

                break;

            case DayCycleState.Night:
                if (timeOfDay <= timeOfCycleInSecond / 4 || timeOfDay >= timeOfCycleInSecond / 4 * 3)
                {
                    _cycleState = DayCycleState.Day;
                    StartCoroutine(_CoroutineNightBegin());
                    MoodManager.instance?.CalculateAverageMood();
                }

                break;
        }
    }

    private IEnumerator _CoroutineDayBegin()
    {
        if (!UIDayCycleManager.Instance)
        {
            DayCycleEvents.OnNightStart.Invoke();
            yield break;
        }
            
        UIDayCycleManager.Instance.StartDayTransition();
        while (UIDayCycleManager.Instance.IsTransitionRunning)
        {
            yield return null;
        }

        DayCycleEvents.OnNightStart.Invoke();
    }

    private IEnumerator _CoroutineNightBegin()
    {
        if (!UIDayCycleManager.Instance)
        {
            DayCycleEvents.OnDayStart.Invoke();
            yield break;
        }
        
        UIDayCycleManager.Instance.StartNightTransition();
        while (UIDayCycleManager.Instance.IsTransitionRunning)
        {
            yield return null;
        }
        
        DayCycleEvents.OnDayStart.Invoke();
    }

    public void SetTime(float time)
    {
        timeOfDay = time;
    }

    private void Multiplicator()
    {
        timeMultiplicator += InputManager.Instance.editorMultiplicatorValue.action.ReadValue<float>();

        if (timeMultiplicator >= 100) timeMultiplicator = 100;
        if (timeMultiplicator <= 0) timeMultiplicator = 0;
    }
}