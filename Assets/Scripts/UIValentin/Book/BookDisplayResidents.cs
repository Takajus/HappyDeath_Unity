using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookDisplayResidents : MonoBehaviour
{
    [SerializeField] private List<GameObject> residentSlots;

    [SerializeField] List<GameObject> Pages;
    [SerializeField] TextMeshProUGUI pageNumberText;
    int currentPageNumber = 0;

    [SerializeField] GameObject leftSide;

    private void OnEnable()
    {
        RefreshResidentSlot();
        if (residentSlots.Count <= 0)
        {
            SetResidentSlot();
            AssignResidents();
        }
        residentSlots[0].GetComponent<ButtonDisplayResidents>().DisplayInformations();
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
            if (InventoryManager.Instance.inventoryDatabase.unlockedResidents.Count <= i)
            {
                break;
            }

            residentSlots[i].GetComponent<ButtonDisplayResidents>().SetScriptableRecipe(InventoryManager.Instance.inventoryDatabase.unlockedResidents[i]);
        }
    }

    private void SetResidentSlot()
    {
        residentSlots.Clear();

        if (HUDManager.Instance.switchBookPanel.IsNewPageNeeded(HUDManager.Instance.inventoryManager.inventoryDatabase.unlockedResidents.Count, Pages.Count))
        {
            GameObject createdPage = HUDManager.Instance.switchBookPanel.CreateResidentsPage(leftSide);
            Pages.Add(createdPage);
        }

        foreach (var page in Pages)
        {
            for (int i = 0; i < page.transform.childCount; i++)
            {
                for (int j = 0; j < page.transform.GetChild(i).transform.childCount; j++)
                {
                    residentSlots.Add(page.transform.GetChild(i).transform.GetChild(j).gameObject);
                }
            }

            page.SetActive(false);
        }

        Pages[0].SetActive(true);
    }

    public void RefreshResidentSlot()
    {
        /*for (int i = 0; i < InventoryManager.Instance.Inventory.Count; i++)
        {
            residentSlots[i].item = InventoryManager.Instance.Inventory[i];
            residentSlots[i].Refresh();
        }*/
    }

    public void UI_PreviousPage()
    {
        Pages[currentPageNumber].SetActive(false);
        currentPageNumber--;

        if (currentPageNumber < 0)
        {
            currentPageNumber = Pages.Count - 1;
        }
        Pages[currentPageNumber].SetActive(true);

        pageNumberText.text = (currentPageNumber + 1).ToString();
    }

    public void UI_NextPage()
    {
        Pages[currentPageNumber].SetActive(false);
        currentPageNumber++;

        if (currentPageNumber >= Pages.Count)
        {
            currentPageNumber = 0;
        }

        Pages[currentPageNumber].SetActive(true);

        pageNumberText.text = (currentPageNumber + 1).ToString();
    }
}
