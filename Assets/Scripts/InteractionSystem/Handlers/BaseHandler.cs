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
    public Material outlineMat;

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
            if (Vector3.Distance(PlayerController.Instance.transform.position, hit.transform.position) > 5)
                return null;

             mouseIsTargeting = true;
             return hit.collider.gameObject;
        }
        else
        {
            mouseIsTargeting = false;

            if (mouseTarget && mouseTarget != target)
                UnHoverTarget(mouseTarget);
            mouseTarget = null;

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

            if (target && mouseTarget != target)
                UnHoverTarget(target);
            target = null;

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
        if (IsInteracting)
        {
            if (InputManager.Instance.gameCancelAction.action.triggered)
            {
                Select(null);
                HoverTarget(mouseTarget);

                if (mouseTarget != target)
                {
                    HoverTarget(target);
                }
            }
        }
        else if (InputManager.Instance.gameCancelAction.action.triggered)
            InteractionManager.Instance.InteruptInteraction();

        if (HUDManager.IsOpen)
            return;

        if (mouseTarget != GetMouseTarget() && mouseIsTargeting)
            UpdateMouseTarget();

        if (target != GetSphereTarget() && isTargeting)
            UpdateSphereTarget();

        if (!target && !mouseTarget)
            Select(null);

        if (InputManager.Instance.gameMouseInteractAction.action.triggered)
            Select(mouseTarget);
        else if (InputManager.Instance.gameInteractAction.action.triggered)
            Select(target);
    }

    protected virtual void UpdateMouseTarget()
    {
        if (mouseTarget != currentInteractedObject && mouseTarget != target)
        {
            UnHoverTarget(mouseTarget);
        }

        mouseTarget = GetMouseTarget();

        if (mouseTarget != currentInteractedObject && mouseTarget != target)
        {
            HoverTarget(mouseTarget);
        }
    }

    protected virtual void UpdateSphereTarget()
    {
        if (target != currentInteractedObject && mouseTarget != target)
        {
            UnHoverTarget(target);
        }

        target = GetSphereTarget();

        if (target != currentInteractedObject && mouseTarget != target)
        {
            HoverTarget(target);
        }
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

    public virtual void InitializeHandler()
    {

    }

    protected virtual bool HasWantedType(GameObject obj) => true;

    protected virtual void HoverTarget(GameObject target) { AddHoverMat(target); }

    protected virtual void UnHoverTarget(GameObject target) { RemoveHoverMat(target); }

    protected virtual void SelectTarget(GameObject target) { }

    protected virtual void UnSelectTarget(GameObject target) { }

    protected virtual void AddHoverMat(GameObject target)
    {
        if (target == null || target.GetComponent<Tile>())
            return;

        foreach (var renderer in target.GetComponentsInChildren<MeshRenderer>())
        {
            List<Material> materials = new List<Material>();

            foreach (var material in renderer.materials)
            {
                materials.Add(material);
            }

            materials.Add(outlineMat);
            renderer.materials = materials.ToArray();
        }
    }

    protected virtual void RemoveHoverMat(GameObject target)
    {
        if (target == null || target.GetComponent<Tile>())
            return;

        foreach (var renderer in target.GetComponentsInChildren<MeshRenderer>())
        {
            List<Material> materials = new List<Material>();

            foreach (var material in renderer.materials)
            {
                materials.Add(material);
            }

            materials.RemoveAll(e => e.shader == outlineMat.shader);
            renderer.materials = materials.ToArray();
        }
    }
}
