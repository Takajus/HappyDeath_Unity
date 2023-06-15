using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    public static InteractionManager instance { get; private set; }
    public static InteractionManager Instance { get { if (instance == null) instance = FindObjectOfType<InteractionManager>(); return instance; } }
    public enum InteractionMode { Interact, Cut, Dig, Mine, Place };
    public static InteractionMode interactionMode { get; private set; }
    public static ResidentData transportedSoul;

    public InteractHandler interactHandler { get; private set; }
    public PlacementHandler placementHandler { get; private set; }
    public ShovelHandler shovelHandler { get; private set; }
    public AxeHandler axeHandler { get; private set; }


    private void Awake()
    {
        interactHandler = GetComponent<InteractHandler>();
        placementHandler = GetComponent<PlacementHandler>();
        shovelHandler = GetComponent<ShovelHandler>();
        axeHandler = GetComponent<AxeHandler>();
    }

    void Update()
    {
        DetermineInteractionMode();
        GetCurrentHandler().HandleInteractable();
    }

    void DetermineInteractionMode()
    {
        InteractionMode temp = InteractionMode.Interact;

        if (InventoryManager.HeldItem)
        {
            if (InventoryManager.HeldItem.itemType == Item.ItemType.BUILD)
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
            }
        }

        if (interactionMode != temp)
            ChangeInteractionMode(temp);
    }

    void ChangeInteractionMode(InteractionMode newInteractionMode)
    {
        GetCurrentHandler().ClearHandler();
        interactionMode = newInteractionMode;
        GetCurrentHandler().InitializeHandler();
    }

    public void InteruptInteraction()
    {
        ChangeInteractionMode(InteractionMode.Interact);
        InventoryManager.HeldItem = null;
    }

    public Material GetHoverMat()
    {
        return GetCurrentHandler().outlineMat;
    }

    BaseHandler GetCurrentHandler()
    {
        switch (interactionMode)
        {
            case InteractionMode.Interact:
                return interactHandler;
            case InteractionMode.Cut:                
                return axeHandler;
            case InteractionMode.Dig:
                return shovelHandler;
            case InteractionMode.Place:
                return placementHandler;
            default:
                return interactHandler;
        }
    }
}
