using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractHandler : BaseHandler
{
    public static ResidentData transportedResident;

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

        if (transportedResident != null)
        {
            //Do something to clear the fact that i am transporting a soul
            transportedResident = null;
        }
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
