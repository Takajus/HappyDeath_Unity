using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CraftActivationManager : MonoBehaviour
{
    public static bool IsOpen = false;
    [SerializeField] CraftingManager craftManager;

    public void OpenCraftingTable()
    {
        if (BookActivationManager.IsOpen)
            return;

        IsOpen = true;
        craftManager.gameObject.SetActive(true);
    }

    public void CloseCraftingTable()
    {
        IsOpen = false;
        craftManager.gameObject.SetActive(false);
    }

}
