using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class BaseChanneler : MonoBehaviour
{
    [SerializeField] string actionName = "Channeling";
    [SerializeField] float channelingTime = .5f;
    protected float timeLeft = .5f;

    protected bool isChanneling;

    protected void CancelChanneling()
    {
        if (!isChanneling)
            return;

        ChannelingHUD.Instance.EndChanneling();
        StopAllCoroutines();
        timeLeft = channelingTime;
        isChanneling = false;
        PlayerController.Instance.SetAction(false);
    }

    protected virtual void ChannelingComplete()
    {
        //Override dans le script child avec ce qu'il faut
    }

    protected IEnumerator StartChanneling()
    {
        PlayerController.Instance.SetAction(true);

        ChannelingHUD.Instance.StartChanneling(actionName);
        isChanneling = true;
        float percentage = 0;
        timeLeft = channelingTime;
        while (timeLeft > 0)
        {
            if (PlayerController.Instance.isMoving || HUDManager.IsOpen)
                CancelChanneling();

            timeLeft -= Time.deltaTime;
            percentage = 1 - timeLeft / channelingTime;
            ChannelingHUD.Instance.UpdateChanneling(percentage);
            yield return null;
        }
        
        PlayerController.Instance.SetAction(false);
        ChannelingComplete();
        ChannelingHUD.Instance.EndChanneling();
        isChanneling = false;

        yield return null;
    }
}
