using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructHandler : BaseHandler
{
    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<Build>() != null)
            return true;

        return false;
    }

    protected override void HoverTarget(GameObject target)
    {
        target?.GetComponent<Build>()?.Hover();
    }

    protected override void UnHoverTarget(GameObject target)
    {
        if (target == null)
        {
            Debug.LogWarning("Target is null");
            return;
        }

        target?.GetComponent<Build>()?.UnHover();
    }

    protected override void SelectTarget(GameObject target)
    {
        currentInteractedObject = target;
        currentInteractedObject?.GetComponent<Build>()?.Interact();
    }

    protected override void UnSelectTarget(GameObject target)
    {
        currentInteractedObject?.GetComponent<Build>()?.EndInteract();
        currentInteractedObject = null;
    }
}
