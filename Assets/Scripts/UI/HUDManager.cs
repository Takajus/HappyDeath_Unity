using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDManager : MonoBehaviour
{
    private static HUDManager instance;
    public static HUDManager Instance { get { if (instance == null) instance = FindObjectOfType<HUDManager>(); return instance; } }

    public InventoryManager inventoryManager;
    public CraftingManager craftingManager;
    public SwitchBookPanel switchBookPanel;
    public QuestSystem questSystem;
    public DisplayResidentStock displayResidentStock;

    [SerializeField] GameObject pannelInventory;
    [SerializeField] GameObject pannelRecipies;
    [SerializeField] GameObject pannelResidents;

    public static bool IsOpen { get => (isBookOpen || isCraftOpen || DisplayResidentStock.IsOpen || MissyQuest.isDialogOpen || TombUI.IsOpen); }

    public static bool isBookOpen = false;
    public static bool isCraftOpen = false;

    private void OnEnable()
    {
        InputManager.Instance.uiInventoryAction.action.performed += ToggleInventory;
    }
    
    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.uiInventoryAction.action.performed -= ToggleInventory;
        }
    }

    public static InventoryManager GetInventoryManager()
    {
        return Instance.inventoryManager;
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        ToggleInventory(!isBookOpen);
    }

    public void ToggleInventory(bool state)
    {
        isBookOpen = state;

        DesActivatePanel();
        pannelInventory.SetActive(state);
        switchBookPanel.gameObject.SetActive(state);
    }

    private void ToggleCraft(InputAction.CallbackContext context)
    {
        ToggleCraft(!isCraftOpen);
    }

    public void ToggleCraft(bool state)
    {
        isCraftOpen = state;
        DesActivatePanel();
        craftingManager.gameObject.SetActive(state);
    }

    private void DesActivatePanel()
    {
        pannelInventory.SetActive(false);
        pannelRecipies.SetActive(false);
        pannelResidents.SetActive(false);
    }

}
