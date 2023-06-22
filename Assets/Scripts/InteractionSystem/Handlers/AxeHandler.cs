using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHandler : BaseHandler
{
    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<ICuttable>() != null)
            return true;

        return false;
    }

    protected override void HoverTarget(GameObject target)
    {
        target?.GetComponent<ICuttable>()?.CutHover();
        AddHoverMat(target);
    }

    protected override void UnHoverTarget(GameObject target)
    {
        target?.GetComponent<ICuttable>()?.CutUnHover();
        RemoveHoverMat(target);
    }

    protected override void SelectTarget(GameObject target)
    {
        target?.GetComponent<ICuttable>()?.Cut();
    }
}
