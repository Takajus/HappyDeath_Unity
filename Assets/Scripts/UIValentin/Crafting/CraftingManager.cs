using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

    [SerializeField] Image imageIngredient_1;
    [SerializeField] Image imageIngredient_2;
    [SerializeField] Image imageIngredient_3;

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
                craftingSlot.Add(verticalBox.transform.GetChild(i).transform.GetChild(j).transform.GetChild(0).gameObject);
            } 
        }
    }

    public void UI_OnClick()
    {
        if (selectedRecipe.scriptableRecipe.ingredient1.IngredientAmount > 0)
        {
            imageIngredient_1.sprite = selectedRecipe.scriptableRecipe.ingredient1.ingredientType.Sprite;
            ingredientAmountNeeded_1.text = selectedRecipe.scriptableRecipe.ingredient1.IngredientAmount.ToString();
            ingredientAmountOwned_1.text = HUDManager.GetInventoryManager().GetIngredientAmount(selectedRecipe.scriptableRecipe.ingredient1.ingredientType).ToString();
        }
        else
        {
            InventoryManager.Instance.parentIngredient_1.SetActive(false);
        }

        if (selectedRecipe.scriptableRecipe.ingredient2.IngredientAmount > 0)
        {
            imageIngredient_2.sprite = selectedRecipe.scriptableRecipe.ingredient2.ingredientType.Sprite;
            ingredientAmountNeeded_2.text = selectedRecipe.scriptableRecipe.ingredient2.IngredientAmount.ToString();
            ingredientAmountOwned_2.text = HUDManager.GetInventoryManager().GetIngredientAmount(selectedRecipe.scriptableRecipe.ingredient2.ingredientType).ToString();
        }
        else
        {
            InventoryManager.Instance.parentIngredient_2.SetActive(false);
        }

        if (selectedRecipe.scriptableRecipe.ingredient3.IngredientAmount > 0)
        {
            imageIngredient_3.sprite = selectedRecipe.scriptableRecipe.ingredient3.ingredientType.Sprite;
            ingredientAmountNeeded_3.text = selectedRecipe.scriptableRecipe.ingredient3.IngredientAmount.ToString();
            ingredientAmountOwned_3.text = HUDManager.GetInventoryManager().GetIngredientAmount(selectedRecipe.scriptableRecipe.ingredient3.ingredientType).ToString();
        }
        else
        {
            InventoryManager.Instance.parentIngredient_3.SetActive(false);
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
        
        if (selectedRecipe.ScriptableRecipe.ingredient1.IngredientAmount > 0)
            ingredientAmountNeeded_1.text = selectedRecipe.ScriptableRecipe.ingredient1.IngredientAmount.ToString();
        else
            ingredientAmountNeeded_1.text = "";


        if (selectedRecipe.ScriptableRecipe.ingredient2.IngredientAmount > 0)
            ingredientAmountNeeded_2.text = selectedRecipe.ScriptableRecipe.ingredient2.IngredientAmount.ToString();
        else
            ingredientAmountNeeded_2.text = "";

        if (selectedRecipe.ScriptableRecipe.ingredient3.IngredientAmount > 0)
            ingredientAmountNeeded_3.text = selectedRecipe.ScriptableRecipe.ingredient3.IngredientAmount.ToString();
        else
            ingredientAmountNeeded_3.text = "";

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
            RefreshIngredientsOwned();
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
