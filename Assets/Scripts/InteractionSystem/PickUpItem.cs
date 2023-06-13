using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpItem : MonoBehaviour, IInteractable
{

    [SerializeField] GameObject E_Input;
    [SerializeField] Item resourceToPick;
    [SerializeField] int amountToGive = 1;

    private void PickUp()
    {
        InventoryManager.Instance.AddItem(resourceToPick, amountToGive);
        Destroy(gameObject);
    }

    public void Hover()
    {
        if (E_Input != null)
            E_Input.SetActive(true);
    }

    public void UnHover()
    {
        if (E_Input != null)
            E_Input.SetActive(false);
    }

    public void Interact()
    {
        PickUp();
    }

    public void EndInteract()
    {
        
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }
}
