using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BaseHandler : MonoBehaviour
{
    [Header("Inputs")]
    [SerializeField] InputActionReference interactAction;
    [SerializeField] InputActionReference mouseInteractAction;
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] InputActionReference mousePosition;

    [Header("References")]
    Camera playerCamera;

    [Header("InputHandle")]
    [SerializeField] LayerMask hitMe;
    [SerializeField] bool isTargeting;
    private GameObject target;
    private RaycastHit hit;

    [Header("MouseHandle")]
    [SerializeField] LayerMask hitMeMouse;
    [SerializeField] bool mouseIsTargeting;
    private GameObject mouseTarget;
    private RaycastHit2D hit2D;

    public static GameObject currentInteractedObject;
    public static bool IsInteracting;

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    protected virtual GameObject GetMouseTarget()
    {
        if ((Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(1, 1, 0) * mousePosition.action.ReadValue<Vector2>()), out hit, Mathf.Infinity, hitMeMouse) && HasWantedType(hit.collider.gameObject)))
        {
             mouseIsTargeting = true;
             return hit.collider.gameObject;
        }
        else
        {
            mouseIsTargeting = false;
            return null;
        }
    }

    public virtual void HandleInteractable()
    {
        if (mouseTarget != GetMouseTarget() && mouseIsTargeting)
            UpdateMouseTarget();

        if (IsInteracting)
        {
            if (cancelAction.action.triggered)
            {
                UnSelectTarget(currentInteractedObject);
                UnHoverTarget(currentInteractedObject);
                currentInteractedObject = null;
                IsInteracting = false;
            }
        }

        if (mouseInteractAction.action.triggered)
            Select(mouseTarget);
        else if (interactAction.action.triggered)
            Select(target);
    }

    protected virtual void UpdateMouseTarget()
    {
        if (mouseTarget != currentInteractedObject)
            UnHoverTarget(mouseTarget);

        mouseTarget = GetMouseTarget();

        if (mouseTarget != currentInteractedObject)
            HoverTarget(mouseTarget);
    }

    protected virtual void Select(GameObject target)
    {
        if (IsInteracting)
        {
            UnSelectTarget(currentInteractedObject);

            if (currentInteractedObject == target)
            {
                HoverTarget(currentInteractedObject);
            }
            else if (target != null)
            {
                UnHoverTarget(currentInteractedObject);

                SelectTarget(currentInteractedObject);

            }
            else
            {
                UnHoverTarget(currentInteractedObject);
            }
        }
        else if (target != null)
        {
            SelectTarget(currentInteractedObject);
        }
    }

    public virtual void ClearHandler()
    {
        UnSelectTarget(mouseTarget);

        UnHoverTarget(mouseTarget);
    }

    protected virtual bool HasWantedType(GameObject obj)
    {
        if (obj?.GetComponent<IInteractable>() != null)
            return true;

        return false;
    }

    protected virtual void HoverTarget(GameObject target)
    {
        target?.GetComponent<IInteractable>()?.Hover();
    }

    protected virtual void UnHoverTarget(GameObject target)
    {
        target?.GetComponent<IInteractable>()?.UnHover();
    }

    protected virtual void SelectTarget(GameObject target)
    {
        currentInteractedObject = target;
        currentInteractedObject?.GetComponent<IInteractable>()?.Interact();
    }

    protected virtual void UnSelectTarget(GameObject target)
    {
        currentInteractedObject?.GetComponent<IInteractable>()?.EndInteract();
        currentInteractedObject = null;
    }
}
