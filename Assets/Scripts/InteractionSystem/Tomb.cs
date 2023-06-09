using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tomb : MonoBehaviour, IInteractable
{
    public ResidentData residentData { get; private set; }
    public Resident resident;

    public Transform origin;

    public void AssignNPC(ResidentData resident)
    {
        if (residentData != null)
            residentData.isAssign = false;

        residentData = resident;
        residentData.isAssign = true;

        this.resident.ResidentData = residentData;

        Debug.Log("test");
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
