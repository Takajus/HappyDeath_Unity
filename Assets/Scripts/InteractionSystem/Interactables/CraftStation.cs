using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftStation : MonoBehaviour, IInteractable
{
    [SerializeField] CraftActivationManager craftActivationManager;
    public void EndInteract()
    {
        craftActivationManager.CloseCraftingTable();
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }

    public void Hover()
    {
        
    }

    public void Interact()
    {
        craftActivationManager.OpenCraftingTable();
    }

    public void UnHover()
    {
        
    }
}
