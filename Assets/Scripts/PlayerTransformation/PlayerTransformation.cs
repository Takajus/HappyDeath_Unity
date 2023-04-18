using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

public class PlayerTransformation : MonoBehaviour
{
    [SerializeField, Required] private GameObject playerDayModel, playerNightModel;
    [SerializeField] private bool isDay = true;

    [SerializeField] private ParticleSystem transformationVFX;

    [SerializeField] private GameObject book, craft;

    void Start()
    {

        LightingManager.Instance._isDay += IsDay;
        LightingManager.Instance._isNight += IsNight;

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
        if (Input.GetKeyDown(KeyCode.N))
        {
            if (isDay)
                isDay = false;
            else
                isDay = true;

            Transformation();
        }

        if (Input.GetKeyDown(KeyCode.B))
        {
            if (book.activeInHierarchy)
                book.SetActive(false);
            else
                book.SetActive(true);
        }
        
        if (Input.GetKeyDown(KeyCode.V))
        {
            if (craft.activeInHierarchy)
                craft.SetActive(false);
            else
                craft.SetActive(true);
        }

    }

    private void IsDay()
    {
        if (isDay == true)
            return;

        Debug.Log("Is Day");

        isDay = true;
        Transformation();
    }

    private void IsNight()
    {
        if (isDay == false)
            return;

        Debug.Log("Is Night");

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
