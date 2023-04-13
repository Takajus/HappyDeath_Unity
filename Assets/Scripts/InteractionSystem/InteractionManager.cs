using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class InteractionManager : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] InputActionReference interactAction;
    [SerializeField] InputActionReference mouseInteractAction;
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] InputActionReference mousePosition;

    [Header("References")]
    [SerializeField] Camera playerCamera;

    [Header("InputHandle")]
    [SerializeField] LayerMask hitMe;
    [SerializeField] bool isTargeting;
    private GameObject target;
    private IInteractable interactableTarget;
    private RaycastHit hit;

    [Header("MouseHandle")]
    [SerializeField] LayerMask hitMeMouse;
    [SerializeField] bool mouseIsTargeting;
    private GameObject mouseTarget;
    private IInteractable mouseInteractableTarget;
    private RaycastHit2D hit2D;

    [HideInInspector] public bool updateHover;
    public static GameObject currentInteractedObject;
    public static bool IsInteracting;

    void Update()
    {
        Debug.DrawRay(playerCamera.transform.position, playerCamera.transform.forward * 100, Color.green);

        HandleInteract();

        //HandleTargeting();    //Need some changes
        HandleMouseTargeting();
    }

    void HandleTargeting()
    {
        //OverlapCollider instead of raycast
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out hit, Mathf.Infinity, hitMe) && hit.collider.GetComponent<IInteractable>() != null)
        {
            if (!isTargeting)
            {
                isTargeting = true;

                target = hit.collider.gameObject;
                interactableTarget = target.GetComponent<IInteractable>();

                interactableTarget.Hover();
            }
            else if (target != hit.collider.gameObject)
            {
                interactableTarget.UnHover();

                target = hit.collider.gameObject;
                interactableTarget = target.GetComponent<IInteractable>();

                interactableTarget.Hover();
            }

            if (updateHover)
            {
                updateHover = false;
                interactableTarget.UnHover();
                interactableTarget.Hover();
            }
        }
        else
        {
            if (isTargeting)
            {
                isTargeting = false;

                if (target)
                {
                    interactableTarget.UnHover();

                    target = null;
                    interactableTarget = null;
                }
            }
        }
    }

    void HandleMouseTargeting()
    {
        if ((Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(1, 1, 0) * mousePosition.action.ReadValue<Vector2>()), out hit, Mathf.Infinity, hitMeMouse) && hit.collider.GetComponent<IInteractable>() != null))
        {
            if (!mouseIsTargeting)
            {
                mouseIsTargeting = true;

                mouseTarget = hit.collider.gameObject;
                mouseInteractableTarget = mouseTarget.GetComponent<IInteractable>();

                if (mouseTarget != currentInteractedObject)
                    mouseInteractableTarget.Hover();
            }
            else if (mouseTarget != hit.collider.gameObject)
            {
                if (mouseTarget != currentInteractedObject)
                    mouseInteractableTarget.UnHover();

                mouseTarget = hit.collider.gameObject;
                mouseInteractableTarget = mouseTarget.GetComponent<IInteractable>();

                if (mouseTarget != currentInteractedObject)
                    mouseInteractableTarget.Hover();
            }

            if (updateHover)
            {
                updateHover = false;

                if (mouseTarget != currentInteractedObject)
                {
                    mouseInteractableTarget.UnHover();
                    mouseInteractableTarget.Hover();
                }
            }
        }
        else
        {
            if (mouseIsTargeting)
            {
                mouseIsTargeting = false;

                if (mouseTarget)
                {
                    if (mouseTarget != currentInteractedObject)
                        mouseInteractableTarget.UnHover();

                    mouseTarget = null;
                    mouseInteractableTarget = null;
                }
            }
        }
    }

    void HandleInteract()
    {
        if (IsInteracting)
        {
            if (cancelAction.action.triggered)
            {
                currentInteractedObject.GetComponent<IInteractable>().EndInteract();
                currentInteractedObject.GetComponent<IInteractable>().UnHover();
                currentInteractedObject = null;
                IsInteracting = false;
            }
        }
        
        if (mouseInteractAction.action.triggered)
            Select(mouseTarget);
        else if (interactAction.action.triggered)
            Select(target);
    }

    void Select(GameObject target)
    {
        if (IsInteracting)
        {
            currentInteractedObject.GetComponent<IInteractable>().EndInteract();

            if (currentInteractedObject == target)
            {
                currentInteractedObject.GetComponent<IInteractable>().Hover();
                currentInteractedObject = null;
            }
            else if (target != null)
            {
                currentInteractedObject.GetComponent<IInteractable>().UnHover();

                target.GetComponent<IInteractable>().Interact();
                currentInteractedObject = target;
            }
            else
            {
                currentInteractedObject.GetComponent<IInteractable>().UnHover();
                currentInteractedObject = null;
            }
        }
        else if (target != null)
        {
            currentInteractedObject = target;
            target.GetComponent<IInteractable>().Interact();
        }

        IsInteracting = currentInteractedObject != null;
    }
}
