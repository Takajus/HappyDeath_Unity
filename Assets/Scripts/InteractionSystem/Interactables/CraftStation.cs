using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftStation : MonoBehaviour, IInteractable
{
    [SerializeField] GameObject E_Input;
    public void EndInteract()
    {
        HUDManager.Instance.ToggleCraft(false);
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }

    public void Hover()
    {
        E_Input.SetActive(true);
    }

    public void Interact()
    {
        HUDManager.Instance.ToggleCraft(true);
        /*if (QuestSystem.instance.isTutorial && QuestSystem.instance.DemoTaskStat == 2)
        {
            QuestSystem.instance.GetDemoTask(3);
        }*/
    }

    public void UnHover()
    {
        E_Input.SetActive(false);
    }
}
