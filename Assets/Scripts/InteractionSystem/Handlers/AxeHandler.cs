using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeHandler : BaseHandler
{
    public bool axeIsEquiped;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
            axeIsEquiped = !axeIsEquiped;
    }

    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<ICuttable>() != null)
            return true;

        return false;
    }

    protected override void HoverTarget(GameObject target)
    {
        target?.GetComponent<ICuttable>()?.CutHover();
    }

    protected override void UnHoverTarget(GameObject target)
    {
        target?.GetComponent<ICuttable>()?.CutUnHover();
    }

    protected override void SelectTarget(GameObject target)
    {
        target?.GetComponent<ICuttable>()?.Cut();
    }
}
