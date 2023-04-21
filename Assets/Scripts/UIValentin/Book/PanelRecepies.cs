using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PanelRecepies : MonoBehaviour
{
    [SerializeField] InventoryManager inventoryMananger;

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

    public void RefreshIngredientsOwned()
    {
        ingredientOwned_1.text = inventoryMananger.GetIngredientAmount(Stone).ToString();
        ingredientOwned_2.text = inventoryMananger.GetIngredientAmount(Wood).ToString();
        ingredientOwned_3.text = inventoryMananger.GetIngredientAmount(Flower).ToString();
    }
}
