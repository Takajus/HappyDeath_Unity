using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BookDisplayRecipes : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryMananger;
    [SerializeField] List<GameObject> recipesSlot;

    [SerializeField] GameObject verticalBox;
    [SerializeField] TextMeshProUGUI ingredientOwned_1;
    [SerializeField] TextMeshProUGUI ingredientOwned_2;
    [SerializeField] TextMeshProUGUI ingredientOwned_3;

    [SerializeField] private Item Stone;
    [SerializeField] private Item Wood;
    [SerializeField] private Item Flower;

    private void OnEnable()
    {
        RefreshIngredientsOwned();
    }

    private void Start()
    {
        SetRecipesSlot();
    }

    private void SetRecipesSlot()
    {
        recipesSlot.Clear();

        for (int i = 0; i < verticalBox.transform.childCount; i++)
        {
            for (int j = 0; j < verticalBox.transform.GetChild(i).transform.childCount; j++)
            {
                recipesSlot.Add(verticalBox.transform.GetChild(i).transform.GetChild(j).gameObject);
            }
        }
    }

    public void RefreshIngredientsOwned()
    {
        ingredientOwned_1.text = inventoryMananger.GetIngredientAmount(Stone).ToString();
        ingredientOwned_2.text = inventoryMananger.GetIngredientAmount(Wood).ToString();
        ingredientOwned_3.text = inventoryMananger.GetIngredientAmount(Flower).ToString();
    }
}
