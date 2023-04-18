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
    public static bool UIOpen { get => CraftActivationManager.IsOpen || BookActivationManager.IsOpen; }

    private void Awake()
    {
        interactHandler = GetComponent<BaseHandler>();
        placementHandler = GetComponent<PlacementHandler>();
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
                break;
            case InteractionMode.Dig:
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
                break;
            case InteractionMode.Dig:
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
