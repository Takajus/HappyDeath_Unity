using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public abstract class BaseHandler : MonoBehaviour
{
    [Header("References")]
    Camera playerCamera;
    GameObject interactSphere;

    [Header("InputHandle")]
    [SerializeField] LayerMask hitMe;
    [SerializeField] bool isTargeting;
    protected GameObject target;
    private RaycastHit hit;

    [Header("MouseHandle")]
    [SerializeField] LayerMask hitMeMouse;
    [SerializeField] bool mouseIsTargeting;
    protected GameObject mouseTarget;
    private RaycastHit2D hit2D;

    public GameObject currentInteractedObject;
    public virtual bool IsInteracting { get => currentInteractedObject != null; }

    private void Start()
    {
        if (playerCamera == null)
            playerCamera = Camera.main;

        if (interactSphere == null)
            interactSphere = PlayerController.Instance.InteractSphere;
    }

    protected virtual GameObject GetMouseTarget()
    {
        if ((Physics.Raycast(Camera.main.ScreenPointToRay(new Vector3(1, 1, 0) * InputManager.Instance.gameMousePosition.action.ReadValue<Vector2>()), out hit, Mathf.Infinity, hitMeMouse) && HasWantedType(hit.collider.gameObject)))
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

    protected virtual GameObject GetSphereTarget()
    {
        List<Collider> items = Physics.OverlapSphere(interactSphere.transform.position, 1.5f, hitMe)
                                      .OrderBy(e => Vector3.Distance(e.transform.position, interactSphere.transform.position))
                                      .ToList();

        items.RemoveAll(e => HasWantedType(e.gameObject) == false);

        if (items.Count > 0)
        {
            isTargeting = true;
            return items[0].gameObject;
        }
        else
        {
            isTargeting = false;
            return null;
        }
    }

    private void OnDrawGizmosSelected()
    {
        if (Application.isPlaying)
        Gizmos.DrawSphere(interactSphere.transform.position, 1.5f);
    }

    public virtual void HandleInteractable()
    {
        if (mouseTarget != GetMouseTarget() && mouseIsTargeting)
            UpdateMouseTarget();

        if (target != GetSphereTarget() && isTargeting)
            UpdateSphereTarget();

        if (IsInteracting)
            if (InputManager.Instance.gameCancelAction.action.triggered)
            {
                Select(null);
                HoverTarget(mouseTarget);
                HoverTarget(target);
            }

        if (InteractionManager.UIOpen)
            return;

        if (InputManager.Instance.gameMouseInteractAction.action.triggered)
            Select(mouseTarget);
        else if (InputManager.Instance.gameInteractAction.action.triggered)
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

    protected virtual void UpdateSphereTarget()
    {
        if (target != currentInteractedObject)
            UnHoverTarget(target);

        target = GetSphereTarget();

        if (target != currentInteractedObject)
            HoverTarget(target);
    }

    protected virtual void Select(GameObject target)
    {
        if (IsInteracting)
        {
            GameObject previousInteractedObject = currentInteractedObject;
            UnSelectTarget(currentInteractedObject);

            if (previousInteractedObject == target)
            {
                HoverTarget(previousInteractedObject);
            }
            else if (target != null)
            {
                UnHoverTarget(previousInteractedObject);

                SelectTarget(target);

            }
            else
            {
                UnHoverTarget(previousInteractedObject);
            }
        }
        else if (target != null)
        {
            SelectTarget(target);
        }
    }

    public virtual void ClearHandler()
    {
        if (IsInteracting)
            Select(null);

        UnHoverTarget(mouseTarget);
        UnHoverTarget(target);
    }

    protected virtual bool HasWantedType(GameObject obj) => true;

    protected virtual void HoverTarget(GameObject target) { }

    protected virtual void UnHoverTarget(GameObject target) { }

    protected virtual void SelectTarget(GameObject target) { }

    protected virtual void UnSelectTarget(GameObject target) { }
}
