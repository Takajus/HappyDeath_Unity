using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CraftingManager : MonoBehaviour
{
    public static CraftingManager Instance;

    [SerializeField] private List<GameObject> inventorySlot;
    [SerializeField] GameObject verticalBox;

    [SerializeField] CraftSetup selectedRecepie;

    [SerializeField] TextMeshProUGUI ingredientAmountNeeded_1;
    [SerializeField] TextMeshProUGUI ingredientAmountNeeded_2;
    [SerializeField] TextMeshProUGUI ingredientAmountNeeded_3;
    
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_1;
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_2;
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_3;

    public Action<CraftSetup> OnRecepieSelected;
    public Action<CraftSetup> OnItemCraft;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        OnRecepieSelected += RefreshIngredientsNeeded;
        GetRecepiesSlot();
        RefreshRecepiesSlot();
    }

    private void OnEnable()
    {
        RefreshRecepiesSlot();
        GetRecepiesSlot();
        Invoke(nameof(WaitForInstanceToFill), 0.01f);
    }

    //Temp
    private void WaitForInstanceToFill()
    {
        RefreshIngredientsNeeded(inventorySlot[0].GetComponent<CraftSetup>());
        inventorySlot[0].GetComponent<CraftSetup>().DisplayInformations();
    }

    private void GetRecepiesSlot()
    {
        for (int i = 0; i < verticalBox.transform.childCount; i++)
        {
            for (int j = 0; j < verticalBox.transform.GetChild(i).transform.childCount; j++)
            {
                inventorySlot.Add(verticalBox.transform.GetChild(i).transform.GetChild(j).gameObject);
            } 
        }
    }

    private void RefreshRecepiesSlot()
    {
        foreach (var slot in inventorySlot)
        {
            slot.GetComponent<CraftSetup>().Refresh();
        }
    }

    private void RefreshRecepiesSlot(CraftSetup slotToRefresh)
    {
        slotToRefresh.Refresh();
    }

    private void RefreshIngredientsNeeded(CraftSetup newSelected)
    {
        selectedRecepie = newSelected;

        ingredientAmountNeeded_1.text = selectedRecepie.ScriptableCraft.ingredient1.IngredientAmount.ToString();
        ingredientAmountNeeded_2.text = selectedRecepie.ScriptableCraft.ingredient2.IngredientAmount.ToString();
        ingredientAmountNeeded_3.text = selectedRecepie.ScriptableCraft.ingredient3.IngredientAmount.ToString();

        RefreshIngredientsOwned();
    }

    private void RefreshIngredientsOwned()
    {
        ingredientAmountOwned_1.text = InventoryManager.Instance.GetIngredientAmount(selectedRecepie.ScriptableCraft.ingredient1.ingredientType).ToString();
        ingredientAmountOwned_2.text = InventoryManager.Instance.GetIngredientAmount(selectedRecepie.ScriptableCraft.ingredient2.ingredientType).ToString();
        ingredientAmountOwned_3.text = InventoryManager.Instance.GetIngredientAmount(selectedRecepie.ScriptableCraft.ingredient3.ingredientType).ToString();
    }

    public void UI_Craft()
    {
        if (HaveEnoughtIngredients())
        {
            OnItemCraft.Invoke(selectedRecepie);
        }
    }

    private bool HaveEnoughtIngredients()
    {
        bool check1 = InventoryManager.Instance.GetIngredientAmount(selectedRecepie.ScriptableCraft.ingredient1.ingredientType) >= selectedRecepie.ScriptableCraft.ingredient1.IngredientAmount;
        bool check2 = InventoryManager.Instance.GetIngredientAmount(selectedRecepie.ScriptableCraft.ingredient2.ingredientType) >= selectedRecepie.ScriptableCraft.ingredient2.IngredientAmount;
        bool check3 = InventoryManager.Instance.GetIngredientAmount(selectedRecepie.ScriptableCraft.ingredient3.ingredientType) >= selectedRecepie.ScriptableCraft.ingredient3.IngredientAmount;


        if (check1 && check2 && check3)
        {
            return true;
        }
        else
        {
            Debug.Log("You need more Resources");
            return false;
        }
    }
}
