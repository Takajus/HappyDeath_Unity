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

            if (mouseTarget)
            {
                RemoveHoverMat(mouseTarget);
                UnHoverTarget(mouseTarget);
            }
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

            if (target)
            {
                RemoveHoverMat(target);
                UnHoverTarget(target);
            }
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
            if (InputManager.Instance.gameCancelAction.action.triggered)
            {
                Select(null);
                AddHoverMat(mouseTarget);
                HoverTarget(mouseTarget);

                if (mouseTarget != target)
                {
                    HoverTarget(target);
                    AddHoverMat(target);
                }
            }

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
        if (mouseTarget != currentInteractedObject)
        {
            RemoveHoverMat(mouseTarget);
            UnHoverTarget(mouseTarget);
        }

        mouseTarget = GetMouseTarget();

        if (mouseTarget != currentInteractedObject)
        {
            AddHoverMat(mouseTarget);
            HoverTarget(mouseTarget);
        }
    }

    protected virtual void UpdateSphereTarget()
    {
        if (target != currentInteractedObject)
        {
            RemoveHoverMat(target);
            UnHoverTarget(target);
        }

        target = GetSphereTarget();

        if (target != currentInteractedObject)
        {
            AddHoverMat(target);
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
                AddHoverMat(previousInteractedObject);
                HoverTarget(previousInteractedObject);
            }
            else if (target != null)
            {
                RemoveHoverMat(previousInteractedObject);
                UnHoverTarget(previousInteractedObject);

                SelectTarget(target);

            }
            else
            {
                RemoveHoverMat(previousInteractedObject);
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

    protected virtual void HoverTarget(GameObject target) { }

    protected virtual void UnHoverTarget(GameObject target) { }

    protected virtual void SelectTarget(GameObject target) { }

    protected virtual void UnSelectTarget(GameObject target) { }

    void AddHoverMat(GameObject target)
    {
        if (target == null)
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

    void RemoveHoverMat(GameObject target)
    {
        if (target == null)
            return;

        foreach (var renderer in target.GetComponentsInChildren<MeshRenderer>())
        {
            List<Material> materials = new List<Material>();

            foreach (var material in renderer.materials)
            {
                materials.Add(material);
            }

            materials.Remove(materials.Last());
            renderer.materials = materials.ToArray();
        }
    }
}
