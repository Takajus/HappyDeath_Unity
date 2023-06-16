using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonDisplayInventory : MonoBehaviour
{
    [System.Serializable]
    public struct SetupButton
    {
        public Image buttonImage;
        public Image imageItem;
        public TextMeshProUGUI name;
        public TextMeshProUGUI textDescription;
        public TextMeshProUGUI textAmount;
    }

    public SetupButton setupButton;
    public Item item;

    private void Start()
    {
        Refresh();
        GetVariables();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { UI_Hover(); });
        GetComponent<EventTrigger>().triggers.Add(entry);

        DisplayInformations();
    }

    private void GetVariables()
    {
        setupButton.imageItem = InventoryManager.Instance.inventoryImageItemToShow;
        setupButton.name = InventoryManager.Instance.inventoryTextName;
        setupButton.textDescription = InventoryManager.Instance.inventoryTextDescription;
    }

    public void Refresh()
    {
        if (item == null)
            return;

        setupButton.buttonImage.sprite = item.Sprite;
        setupButton.textAmount.text = InventoryManager.Instance.GetIngredientAmount(item).ToString();
    }

    public void UI_ClickedOnMe()
    {
        PlaceItem();
    }

    private void PlaceItem()
    {
        if (item)
        {
            InventoryManager.HeldItem = item;
            if (item.itemType == Item.ItemType.BUILD)
            {
                InteractionManager.Instance.placementHandler.GiveObject(item.Prefab);
                HUDManager.Instance.ToggleInventory(false);
            }
        }
    }

    public void DisplayInformations()
    {
        if (item == null)
            return;

        if (setupButton.imageItem == null)
        {
            return;
        }

        setupButton.imageItem.color = Color.white;
        setupButton.imageItem.sprite = item.Sprite;
        setupButton.textDescription.text = item.Description;
        setupButton.name.text = item.Name;
        setupButton.textAmount.text = InventoryManager.Instance.GetIngredientAmount(item).ToString();
    }

    public void UI_Hover()
    {
        DisplayInformations();
    }
}

