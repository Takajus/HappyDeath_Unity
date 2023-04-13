using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using LFS.FirstPerson.Gameplay;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField, Required] GameObject playerDayModel, playerNightModel;
    [SerializeField] private bool isDay = true;

    [SerializeField] ParticleSystem transformationVFX;

    void Start()
    {

        LightingManager.Instance.isDay += IsDay;
        LightingManager.Instance.isNight += IsNight;

        if (isDay)
        {
            playerDayModel.SetActive(true);
            playerNightModel.SetActive(false);
        }
        else
        {
            playerDayModel.SetActive(false);
            playerNightModel.SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Test
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (isDay)
                isDay = false;
            else
                isDay = true;

            Transformation();
        }

    }

    private void IsDay()
    {
        if (isDay == true)
            return;

        isDay = true;
        Transformation();
    }

    private void IsNight()
    {
        if (isDay == false)
            return;

        isDay = false;
        Transformation();
    }

    private void Transformation(/*bool timeIndex*/)
    {
        /*if (*//*timeIndex == *//*isDay)
            return;*/

        StartCoroutine(TransformationStart());
    }

    private IEnumerator TransformationStart()
    {
        PlayerController.Instance.DisablePlayer();

        // Start Transition/Transformation VFX/Animation
        ParticleSystem particule = transformationVFX;
        particule.Play();

        //Debug.Log("Transformation Start");
        yield return new WaitForSeconds(particule.main.duration / 2);
        SwitchModel();
        yield return new WaitForSeconds(particule.main.duration / 2);
        //Debug.Log("Transformation End");

        PlayerController.Instance.EnablePlayer();
    }

    private void SwitchModel()
    {
        if (isDay/*!playerDayModel.activeInHierarchy*/)
        {
            playerDayModel.SetActive(true);
            playerNightModel.SetActive(false);
        }
        else
        {
            playerDayModel.SetActive(false);
            playerNightModel.SetActive(true);
        }
    }
}
