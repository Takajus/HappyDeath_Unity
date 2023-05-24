using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShovelHandler : BaseHandler
{
    public bool shovelIsEquiped;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
            shovelIsEquiped = !shovelIsEquiped;
    }

    protected override bool HasWantedType(GameObject obj)
    {
        if (obj.GetComponent<IDiggable>() != null)
            return true;

        return false;
    }

    protected override void HoverTarget(GameObject target)
    {
        target?.GetComponent<IDiggable>()?.DigHover();
    }

    protected override void UnHoverTarget(GameObject target)
    {
        target?.GetComponent<IDiggable>()?.DigUnHover();
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
