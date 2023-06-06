using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTarget : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    
    [Header("Default Camera")]
    [SerializeField] 
    private GameObject defaultCamaraTarget;

    [Header("Dialogue Camera")] [SerializeField]
    private GameObject dialogueCameraTarget;

    public static DialogueTarget Instance {
        get;
        private set;
    }

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;
        
        SwitchTarget(true);
    }

    public void SwitchTarget(bool bIsDefault)
    {
        if (bIsDefault)
        {
            dialogueCameraTarget.SetActive(false);
            defaultCamaraTarget.SetActive(true);
        }
        else
        {
            defaultCamaraTarget.SetActive(false);
            dialogueCameraTarget.SetActive(true);
        }
    }
}
