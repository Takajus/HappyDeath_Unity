using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class CraftingManager : MonoBehaviour
{
    private static CraftingManager instance;
    public static CraftingManager Instance { get { if (instance == null) instance = FindObjectOfType<CraftingManager>(); return instance; } }

    [SerializeField] private List<GameObject> craftingSlot;
    [SerializeField] private List<GameObject> bookCraftingSlot;
    [SerializeField] GameObject verticalBox;

    [SerializeField] CraftSetup selectedRecipe;

    [SerializeField] TextMeshProUGUI ingredientAmountNeeded_1;
    [SerializeField] TextMeshProUGUI ingredientAmountNeeded_2;
    [SerializeField] TextMeshProUGUI ingredientAmountNeeded_3;
    
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_1;
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_2;
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_3;

    public Action<CraftSetup> OnRecipeSelected;
    public Action<CraftSetup> OnItemCraft;

    private void Awake()
    {
        GetRecipesSlot();
        AssignRecipes();
        RefreshRecipesSlot();
    }

    private void Start()
    {
        OnRecipeSelected += RefreshIngredientsNeeded;
    }

    private void OnEnable()
    {
        RefreshRecipesSlot();
        GetRecipesSlot();
        Invoke(nameof(WaitForInstanceToFill), 0.01f);
    }

    //Temp
    private void WaitForInstanceToFill()
    {
        RefreshIngredientsNeeded(craftingSlot[0].GetComponent<CraftSetup>());
        craftingSlot[0].GetComponent<CraftSetup>().DisplayInformations();
    }

    private void AssignRecipes()
    {
        for (int i = 0; i < craftingSlot.Count; i++)
        {
            if (HUDManager.GetInventoryManager().inventoryDatabase.allRecipes.Count <= i)
            {
                break;
            }
            craftingSlot[i].GetComponent<CraftSetup>().SetScriptableRecipe(HUDManager.GetInventoryManager().inventoryDatabase.allRecipes[i]);
        }
    }

    private void GetRecipesSlot()
    {
        for (int i = 0; i < verticalBox.transform.childCount; i++)
        {
            for (int j = 0; j < verticalBox.transform.GetChild(i).transform.childCount; j++)
            {
                craftingSlot.Add(verticalBox.transform.GetChild(i).transform.GetChild(j).gameObject);
            } 
        }
    }

    private void RefreshRecipesSlot()
    {
        foreach (var slot in craftingSlot)
        {
            slot.GetComponent<CraftSetup>().Refresh();
        }
    }

    private void RefreshRecipesSlot(CraftSetup slotToRefresh)
    {
        slotToRefresh.Refresh();
    }

    private void RefreshIngredientsNeeded(CraftSetup newSelected)
    {
        selectedRecipe = newSelected;

        ingredientAmountNeeded_1.text = selectedRecipe.ScriptableRecipe.ingredient1.IngredientAmount.ToString();
        ingredientAmountNeeded_2.text = selectedRecipe.ScriptableRecipe.ingredient2.IngredientAmount.ToString();
        ingredientAmountNeeded_3.text = selectedRecipe.ScriptableRecipe.ingredient3.IngredientAmount.ToString();

        RefreshIngredientsOwned();
    }

    public void RefreshIngredientsOwned()
    {
        ingredientAmountOwned_1.text = HUDManager.GetInventoryManager().GetIngredientAmount(selectedRecipe.ScriptableRecipe.ingredient1.ingredientType).ToString();
        ingredientAmountOwned_2.text = HUDManager.GetInventoryManager().GetIngredientAmount(selectedRecipe.ScriptableRecipe.ingredient2.ingredientType).ToString();
        ingredientAmountOwned_3.text = HUDManager.GetInventoryManager().GetIngredientAmount(selectedRecipe.ScriptableRecipe.ingredient3.ingredientType).ToString();
    }

    public void UI_Craft()
    {
        if (HaveEnoughtIngredients())
        {
            OnItemCraft.Invoke(selectedRecipe);
        }
    }

    private bool HaveEnoughtIngredients()
    {
        bool check1 = InventoryManager.Instance.GetIngredientAmount(selectedRecipe.ScriptableRecipe.ingredient1.ingredientType) >= selectedRecipe.ScriptableRecipe.ingredient1.IngredientAmount;
        bool check2 = InventoryManager.Instance.GetIngredientAmount(selectedRecipe.ScriptableRecipe.ingredient2.ingredientType) >= selectedRecipe.ScriptableRecipe.ingredient2.IngredientAmount;
        bool check3 = InventoryManager.Instance.GetIngredientAmount(selectedRecipe.ScriptableRecipe.ingredient3.ingredientType) >= selectedRecipe.ScriptableRecipe.ingredient3.IngredientAmount;


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
