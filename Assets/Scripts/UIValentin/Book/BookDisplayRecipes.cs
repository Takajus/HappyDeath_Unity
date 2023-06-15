using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookDisplayRecipes : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryManager;
    [SerializeField] List<GameObject> recipesSlot;
    [SerializeField] List<GameObject> Pages;
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

        if (recipesSlot.Count > 0)
        {
            recipesSlot[0].GetComponent<DisplayRecipes>().DisplayInformations();
            recipesSlot[0].GetComponent<DisplayRecipes>().UI_ClickedOnMe();
        }
    }

    private void Start()
    {
        if (recipesSlot.Count <= 0)
        {
            SetRecipesSlot();
            AssignRecipes();
        }
    }

    private void SetRecipesSlot()
    {
        recipesSlot.Clear();

        /*for (int i = 0; i < verticalBox.transform.childCount; i++)
        {
            for (int j = 0; j < verticalBox.transform.GetChild(i).transform.childCount; j++)
            {
                recipesSlot.Add(verticalBox.transform.GetChild(i).transform.GetChild(j).gameObject);
            }
        }*/

        if (HUDManager.Instance.switchBookPanel.IsNewPageNeeded(HUDManager.Instance.inventoryManager.ResidentsInventory.Count, Pages.Count))
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
        }
    }

    private void AssignRecipes()
    {
        for (int i = 0; i < recipesSlot.Count; i++)
        {
            if (inventoryManager.RecipesInventory.Count <= i)
            {
                break;
            }
            recipesSlot[i].GetComponent<DisplayRecipes>().SetScriptableRecipe(inventoryManager.RecipesInventory[i]);
        }
    }

    public void RefreshIngredientsOwned()
    {
        ingredientOwned_1.text = inventoryManager.GetIngredientAmount(Stone).ToString();
        ingredientOwned_2.text = inventoryManager.GetIngredientAmount(Wood).ToString();
        ingredientOwned_3.text = inventoryManager.GetIngredientAmount(Flower).ToString();
    }
}
