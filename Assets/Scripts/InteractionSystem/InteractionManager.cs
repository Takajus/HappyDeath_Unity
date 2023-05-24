using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance { get; private set; }
    public static InteractionManager Instance { get { if (instance == null) instance = FindObjectOfType<InteractionManager>(); return instance; } }
    public enum InteractionMode { Interact, Cut, Dig, Place };
    public static InteractionMode interactionMode { get; private set; }

    public BaseHandler interactHandler { get; private set; }
    public PlacementHandler placementHandler { get; private set; }
    public ShovelHandler shovelHandler { get; private set; }
    public AxeHandler axeHandler { get; private set; }
    public static bool UIOpen { get => HUDManager.IsOpen || HUDManager.IsOpen; }

    private void Awake()
    {
        interactHandler = GetComponent<BaseHandler>();
        placementHandler = GetComponent<PlacementHandler>();
        shovelHandler = GetComponent<ShovelHandler>();
        axeHandler = GetComponent<AxeHandler>();
    }

    void Update()
    {
        DetermineInteractionMode();

        switch (interactionMode)
        {
            case InteractionMode.Interact:
                interactHandler.HandleInteractable();
                break;
            case InteractionMode.Cut:
                axeHandler.HandleInteractable();
                break;
            case InteractionMode.Dig:
                shovelHandler.HandleInteractable();
                break;
            case InteractionMode.Place:
                placementHandler.HandleInteractable();
                break;
            default:
                interactHandler.HandleInteractable();
                break;
        }
    }

    void DetermineInteractionMode()
    {
        InteractionMode temp = InteractionMode.Interact;

        if (placementHandler.IsInteracting)
        {
            temp = InteractionMode.Place;
        }
        else if (axeHandler.axeIsEquiped)
        {
            temp = InteractionMode.Cut;
        }
        else if (shovelHandler.shovelIsEquiped)
        {
            temp = InteractionMode.Dig;
        }

        /*if (InventoryManager.HeldItem.itemType == Item.ItemType.BUILD)
        {
            temp = InteractionMode.Place;
        }
        else if (InventoryManager.HeldItem.itemType == Item.ItemType.TOOL && InventoryManager.HeldItem.Name == "Axe")
        {
            temp = InteractionMode.Cut;
        }
        else if (InventoryManager.HeldItem.itemType == Item.ItemType.TOOL && InventoryManager.HeldItem.Name == "Shovel")
        {
            temp = InteractionMode.Dig;
        }*/

        if (interactionMode != temp)
            ChangeInteractionMode(temp);
    }

    void ChangeInteractionMode(InteractionMode newInteractionMode)
    {
        switch (interactionMode)
        {
            case InteractionMode.Interact:
                interactHandler.ClearHandler();
                break;
            case InteractionMode.Cut:
                axeHandler.ClearHandler();
                break;
            case InteractionMode.Dig:
                shovelHandler.ClearHandler();
                break;
            case InteractionMode.Place:
                placementHandler.ClearHandler();
                break;
            default:
                interactHandler.ClearHandler();
                break;
        }

        interactionMode = newInteractionMode;
    }
}
