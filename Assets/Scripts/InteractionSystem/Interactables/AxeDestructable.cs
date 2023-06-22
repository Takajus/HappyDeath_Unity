using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AxeDestructable : BaseChanneler, ICuttable
{
    [SerializeField] GameObject Childs;
    public void Cut()
    {
        if (!isChanneling)
            StartCoroutine(StartChanneling());
    }

    protected override void ChannelingComplete()
    {
        Childs.SetActive(true);
        gameObject.SetActive(false);

        InteractionManager.Instance.InteruptInteraction();
    }

    public void CutHover()
    {
        
    }

    public void CutUnHover()
    {
        
    }
}
