using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResidentManager : MonoBehaviour, IInteractable
{
    private static ResidentManager instance;
    public static ResidentManager Instance { get { if (instance == null) instance = FindObjectOfType<ResidentManager>(); return instance; } }

    public TombUI tombUI;
    private Animator anim;

    private void Start()
    {
        tombUI.ToggleDisplay(false);
        HUDManager.Instance.displayResidentStock.ToggleDisplay(false);


        anim = GetComponent<Animator>();
    
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
        anim.SetBool("isOpenned", true);
    }

    public void UnHover()
    {
        anim.SetBool("isOpenned", false);

    }

    public void OpenTombUI(Tomb tomb)
    {
        tombUI.ToggleDisplay(true, tomb);
    }

    public void CloseTombUI()
    {
        tombUI.ToggleDisplay(false);
    }
}
