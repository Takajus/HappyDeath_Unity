using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpItem : BaseChanneler, IInteractable
{
    [SerializeField] Item resourceToPick;
    [SerializeField] int amountToGive = 1;

    private enum PickUpSoundState
    {
        WOOD, 
        ROCK,
        FLOWER
    };

    private PickUpSoundState pickUpSoundState;

    private void PickUp()
    {
        switch(resourceToPick.Name )
        {
            case "Wood":
                AudioManager.Instance.Take_TreeBranch.Post(gameObject);
                break;
            case "Flower":
                AudioManager.Instance.Take_Flower.Post(gameObject);
                break;
            case "Stone":
                AudioManager.Instance.Take_Rock.Post(gameObject);
                break;
            default:
                break;
        }
        
        InventoryManager.Instance.AddItem(resourceToPick, amountToGive);
        Destroy(gameObject);
    }

    protected override void ChannelingComplete()
    {
        PickUp();
    }

    public void Hover()
    {

    }

    public void UnHover()
    {

    }

    public void Interact()
    {
        if (!isChanneling)
            StartCoroutine(StartChanneling());
    }

    public void EndInteract()
    {
        CancelChanneling();
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }
}
