using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookDisplayResidents : MonoBehaviour
{
    [SerializeField] private List<GameObject> residentSlots;
    [SerializeField] GameObject verticalBox;
    private void OnEnable()
    {
        RefreshResidentSlot();
        if (residentSlots.Count <= 0)
        {
            SetResidentSlot();
            AssignResidents();
        }
        residentSlots[0].GetComponent<DisplayResidents>().DisplayInformations();
        residentSlots[0].GetComponent<DisplayResidents>().UI_ClickedOnMe();
    }

    private void Start()
    {
        if (residentSlots.Count <= 0)
        {
            SetResidentSlot();
            AssignResidents();
        }
    }

    private void AssignResidents()
    {
        for (int i = 0; i < residentSlots.Count; i++)
        {
            if (InventoryManager.Instance.inventoryDatabase.allResidents.Count <= i)
            {
                break;
            }

            residentSlots[i].GetComponent<DisplayResidents>().SetScriptableRecipe(InventoryManager.Instance.inventoryDatabase.allResidents[i]);
        }
    }

    private void SetResidentSlot()
    {
        residentSlots.Clear();

        for (int i = 0; i < verticalBox.transform.childCount; i++)
        {
            for (int j = 0; j < verticalBox.transform.GetChild(i).transform.childCount; j++)
            {
                residentSlots.Add(verticalBox.transform.GetChild(i).transform.GetChild(j).gameObject);
            }
        }
    }

    public void RefreshResidentSlot()
    {
        /*for (int i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            residentSlots[i].item = InventoryManager.Instance.Inventory[i];
            residentSlots[i].Refresh();
        }*/
    }
}
