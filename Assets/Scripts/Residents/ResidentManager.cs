using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentManager : MonoBehaviour, IInteractable
{
    private static ResidentManager instance;
    public static ResidentManager Instance { get { if (instance == null) instance = FindObjectOfType<ResidentManager>(); return instance; } }

    public GameObject tombAssignUI;

    private void Start()
    {
        tombAssignUI.SetActive(false);
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }
    
    public void Interact()
    {
        HUDManager.Instance.displayResidentStock.ToggleDisplay(true);
    }

    public void EndInteract()
    {
        HUDManager.Instance.displayResidentStock.ToggleDisplay(false);
    }

    public void Hover()
    {

    }

    public void UnHover()
    {

    }

    public void OpenTombUI(Tomb tomb)
    {
        tombAssignUI.SetActive(true);
    }

    public void CloseTombUI()
    {
        tombAssignUI.SetActive(false);
    }
}
