using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelHandler : BaseHandler
{
    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<IDiggable>() != null)
            return true;

        return false;
    }

    protected override void HoverTarget(GameObject target)
    {
        target?.GetComponent<IDiggable>()?.DigHover();
        AddHoverMat(target);
    }

    protected override void UnHoverTarget(GameObject target)
    {
        target?.GetComponent<IDiggable>()?.DigUnHover();
        RemoveHoverMat(target);
    }

    protected override void SelectTarget(GameObject target)
    {
        if (target.TryGetComponent(out IDiggable diggable))
        {
            if (diggable.IsDug)
                diggable.Fill();
            else
                diggable.Dig();
        }
    }
}
