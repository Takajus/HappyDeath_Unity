using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : BaseHandler
{
    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<IInteractable>() != null)
            return true;

        return false;
    }

    public override void ClearHandler()
    {
        if (IsInteracting)
            Select(null);

        UnHoverTarget(mouseTarget);
        UnHoverTarget(target);
    }

    protected override void HoverTarget(GameObject target)
    {
        target?.GetComponent<IInteractable>()?.Hover();
    }

    protected override void UnHoverTarget(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null");
            return;
        }

        target?.GetComponent<IInteractable>()?.UnHover();
    }

    protected override void SelectTarget(GameObject target)
    {
        currentInteractedObject = target;
        currentInteractedObject?.GetComponent<IInteractable>()?.Interact();
    }

    protected override void UnSelectTarget(GameObject target)
    {
        currentInteractedObject?.GetComponent<IInteractable>()?.EndInteract();
        currentInteractedObject = null;
    }
}
