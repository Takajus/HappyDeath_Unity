using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickaxeHandler : BaseHandler
{
    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<IMinable>() != null)
            return true;

        return false;
    }

    protected override void HoverTarget(GameObject target)
    {
        target?.GetComponent<IMinable>()?.MineHover();
        AddHoverMat(target);
    }

    protected override void UnHoverTarget(GameObject target)
    {
        target?.GetComponent<IMinable>()?.MineUnHover();
        RemoveHoverMat(target);
    }

    protected override void SelectTarget(GameObject target)
    {
        target?.GetComponent<IMinable>()?.Mine();
    }
}
