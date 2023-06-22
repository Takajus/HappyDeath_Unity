using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BookDisplayInventory : MonoBehaviour
{
    [SerializeField] private List<ButtonDisplayInventory> inventorySlot;
    [SerializeField] List<GameObject> Pages;
    [SerializeField] TextMeshProUGUI pageNumberText;
    [SerializeField] GameObject leftSide;
    int currentPageNumber = 0;

    private void OnEnable()
    {
        if (inventorySlot.Count == 0)
        {
            GetInventorySlot();
        }

        RefreshInventorySlot();
        if (inventorySlot.Count > 0)
        {
            inventorySlot[0].DisplayInformations();
            //inventorySlot[0].UI_ClickedOnMe();
        }
    }

    private void Start()
    {
        GetInventorySlot();
        HUDManager.Instance.ToggleInventory(false);
    }


    private void GetInventorySlot()
    {

        if (HUDManager.Instance.switchBookPanel.IsNewPageNeeded(HUDManager.Instance.inventoryManager.inventoryDatabase.items.Count, Pages.Count))
        {
            GameObject createdPage = HUDManager.Instance.switchBookPanel.CreateItemsPage(leftSide);
            Pages.Add(createdPage);
        }

        foreach (var page in Pages)
        {
            for (int i = 0; i < page.transform.childCount; i++)
            {
                for (int j = 0; j < page.transform.GetChild(i).transform.childCount; j++)
                {
                    ButtonDisplayInventory objectToAdd = page.transform.GetChild(i).transform.GetChild(j).GetComponent<ButtonDisplayInventory>();
                    if (!inventorySlot.Contains(objectToAdd))
                        inventorySlot.Add(objectToAdd);
                }
            }
            page.SetActive(false);
        }

        Pages[0].SetActive(true);
    }

    public void RefreshInventorySlot()
    {
        foreach (var item in inventorySlot)
        {
            item.item = null;
            item.setupButton.buttonImage.sprite = null;
            item.setupButton.textAmount.text = "";
            item.setupButton.buttonImage.color = new Color(0,0,0,0);
        }

        for (int i = 0; i < InventoryManager.Instance.ItemsInventory.Count; i++)
        {
            inventorySlot[i].item = InventoryManager.Instance.ItemsInventory[i];
            if (inventorySlot[i].item != null)
                inventorySlot[i].setupButton.buttonImage.color = Color.white;

            inventorySlot[i].Refresh();
        }
    }

    public void UI_PreviousPage()
    {
        Pages[currentPageNumber].SetActive(false);
        currentPageNumber--;

        if (currentPageNumber < 0)
        {
            currentPageNumber = Pages.Count -1;
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
        
        pageNumberText.text = (currentPageNumber +1).ToString();
    }
}
