using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickAxeDestructable : BaseChanneler, IMinable
{
    [SerializeField] GameObject Childs;
    public void Mine()
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

    public void MineHover()
    {
        
    }

    public void MineUnHover()
    {
        
    }
}
