using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDisplayInventory : MonoBehaviour
{
    [SerializeField] private List<ButtonInventory> inventorySlot;
    [SerializeField] GameObject verticalBox;

    private void Start()
    {
        GetInventorySlot();
    }

    private void OnEnable()
    {
        if (inventorySlot.Count == 0)
        {
            GetInventorySlot();
        }

        RefreshInventorySlot();
        inventorySlot[0].DisplayInformations();
        inventorySlot[0].UI_ClickedOnMe();
    }

    private void GetInventorySlot()
    {
        for (int i = 0; i < verticalBox.transform.childCount; i++)
        {
            for (int j = 0; j < verticalBox.transform.GetChild(i).transform.childCount; j++)
            {
                inventorySlot.Add(verticalBox.transform.GetChild(i).transform.GetChild(j).GetComponent<ButtonInventory>());
            }
        }
    }

    public void RefreshInventorySlot()
    {

        foreach (var item in inventorySlot)
        {
            item.item = null;
            item.setupButton.buttonImage.sprite = null;
            item.setupButton.textAmount.text = "";
        }

        for (int i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            inventorySlot[i].item = InventoryManager.Instance.Inventory[i];
            inventorySlot[i].Refresh();
        }
    }
}
