using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookDisplayRecipes : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] List<GameObject> recipesSlot;

    [SerializeField] List<GameObject> Pages;
    [SerializeField] TextMeshProUGUI pageNumberText;
    int currentPageNumber = 0;

    [SerializeField] GameObject leftSide;

    [SerializeField] TextMeshProUGUI ingredientOwned_1;
    [SerializeField] TextMeshProUGUI ingredientOwned_2;
    [SerializeField] TextMeshProUGUI ingredientOwned_3;

    [SerializeField] private Item Stone;
    [SerializeField] private Item Wood;
    [SerializeField] private Item Flower;

    private void OnEnable()
    {
        RefreshIngredientsOwned();
        if (recipesSlot.Count <= 0)
        {
            SetRecipesSlot();
            AssignRecipes();
        }
    }

    private void Start()
    {
        if (recipesSlot.Count > 0)
        {
            recipesSlot[0].GetComponent<ButtonDisplayRecipes>().DisplayInformations();
        }
    }

    private void SetRecipesSlot()
    {
        recipesSlot.Clear();

        if (HUDManager.Instance.switchBookPanel.IsNewPageNeeded(HUDManager.Instance.inventoryManager.inventoryDatabase.unlockedRecipes.Count, Pages.Count))
        {
            GameObject createdPage = HUDManager.Instance.switchBookPanel.CreateRecipesPage(leftSide);
            Pages.Add(createdPage);
        }

        foreach (var page in Pages)
        {
            for (int i = 0; i < page.transform.childCount; i++)
            {
                for (int j = 0; j < page.transform.GetChild(i).transform.childCount; j++)
                {
                    recipesSlot.Add(page.transform.GetChild(i).transform.GetChild(j).gameObject);
                }
            }

            page.SetActive(false);
        }

        Pages[0].SetActive(true);
    }

    private void AssignRecipes()
    {
        for (int i = 0; i < recipesSlot.Count; i++)
        {
            if (inventoryManager.inventoryDatabase.unlockedRecipes.Count <= i)
            {
                break;
            }
            recipesSlot[i].GetComponent<ButtonDisplayRecipes>().SetScriptableRecipe(inventoryManager.inventoryDatabase.unlockedRecipes[i]);
        }
    }

    public void RefreshIngredientsOwned()
    {
        ingredientOwned_1.text = inventoryManager.GetIngredientAmount(Stone).ToString();
        ingredientOwned_2.text = inventoryManager.GetIngredientAmount(Wood).ToString();
        ingredientOwned_3.text = inventoryManager.GetIngredientAmount(Flower).ToString();
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
