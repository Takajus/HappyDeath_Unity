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
    public ShortcutWheel shortcutWheel;

    [SerializeField] GameObject pannelInventory;
    [SerializeField] GameObject pannelRecipies;
    [SerializeField] GameObject pannelResidents;

    public static bool IsOpen { get => (isBookOpen || isCraftOpen || DisplayResidentStock.IsOpen || Missy.isDialogOpen || TombUI.IsOpen || ShortcutWheel.wheelIsOpen); }
    
    public static bool isBookOpen = false;
    public static bool isCraftOpen = false;

    private void OnEnable()
    {
        InputManager.Instance.uiInventoryAction.action.performed += ToggleInventory;
        InputManager.Instance.gameCancelAction.action.performed += CloseUI;
    }
    
    private void OnDisable()
    {
        if (InputManager.Instance != null)
        {
            InputManager.Instance.uiInventoryAction.action.performed -= ToggleInventory;
            InputManager.Instance.gameCancelAction.action.performed -= CloseUI;
        }
    }

    public static InventoryManager GetInventoryManager()
    {
        return Instance.inventoryManager;
    }

    public void CloseUI(InputAction.CallbackContext context)
    {
        isBookOpen = false;
        DesActivatePanel();
        pannelInventory.SetActive(true);
        switchBookPanel.gameObject.SetActive(false);
        InteractionManager.Instance.InteruptInteraction(false);
    }

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        if (isBookOpen)
        {
            CloseUI(context);
            return;
        }

        if (!IsOpen)
        {
            ToggleInventory(!isBookOpen);
        }
    }

    public void ToggleInventory(bool state)
    {
        isBookOpen = state;

        DesActivatePanel();
        pannelInventory.SetActive(state);
        switchBookPanel.gameObject.SetActive(state);
    }

    public void UI_ToggleInventory()
    {
        isBookOpen = !isBookOpen;

        DesActivatePanel();
        pannelInventory.SetActive(isBookOpen);
        switchBookPanel.gameObject.SetActive(isBookOpen);
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
