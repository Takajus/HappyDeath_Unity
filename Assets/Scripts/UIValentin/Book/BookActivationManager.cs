using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BookActivationManager : MonoBehaviour
{
    public static bool IsOpen = false;
    [SerializeField] InputActionReference cancelAction;
    [SerializeField] SwitchBookPanel switchBookPanel;

    void Update()
    {
        if (cancelAction.action.triggered)
        {
            OpenCloseBook();
        }
    }

    public void OpenCloseBook()
    {
        if (CraftActivationManager.IsOpen)
            return;
        
        if (IsOpen)
        {
            switchBookPanel.gameObject.SetActive(false);
        }
        else
        {
            switchBookPanel.gameObject.SetActive(true);
            switchBookPanel.UI_DisplayPanel(0);
        }

        IsOpen = !IsOpen;
    }
}
