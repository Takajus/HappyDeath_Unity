using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tomb : MonoBehaviour, IInteractable
{
    public ResidentData residentData { get; private set; }
    public Resident resident;

    public Transform origin;

    public static Action<ResidentData, Tomb> OnAssignNPC;

    public void AssignNPC(ResidentData resident)
    {
        if (residentData != null)
            residentData.isAssign = false;

        residentData = resident;
        residentData.isAssign = true;

        this.resident.ResidentData = residentData;

        OnAssignNPC?.Invoke(residentData, this);
    }

    public void Interact()
    {
        if (InteractHandler.transportedResident != null)
            AssignNPC(InteractHandler.transportedResident);

        //ResidentManager.Instance.OpenTombUI(this);
    }

    public void EndInteract()
    {
        //ResidentManager.Instance.CloseTombUI();
    }

    public void Hover()
    {

    }

    public void UnHover()
    {

    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }
}
