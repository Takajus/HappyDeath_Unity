using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class HUDManager : MonoBehaviour
{
    public static HUDManager instance { get; private set; }
    public static HUDManager Instance { get { if (instance == null) instance = FindObjectOfType<HUDManager>(); return instance; } }

    public InventoryManager inventoryManager;
    public CraftingManager craftingManager;
    public SwitchBookPanel switchBookPanel;
    public QuestSystem questSystem;

    [SerializeField] GameObject pannelInventory;
    [SerializeField] GameObject pannelRecipies;
    [SerializeField] GameObject pannelResidents;


    public static bool IsOpen = isInventoryOpen || isBookOpen || isCraftOpen;

    public static bool isInventoryOpen = false;
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

    public void ToggleInventory(InputAction.CallbackContext context)
    {
        ToggleInventory(!isInventoryOpen);
    }

    public void ToggleInventory(bool state)
    {
        isInventoryOpen = state;
        ToggleBook(state);
        DesActivatePanel();
        pannelInventory.SetActive(state);
    }

    private void ToggleBook(InputAction.CallbackContext context)
    {
        ToggleBook(!isBookOpen);
    }

    public void ToggleBook(bool state)
    {
        isBookOpen = state;

        DesActivatePanel();
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
