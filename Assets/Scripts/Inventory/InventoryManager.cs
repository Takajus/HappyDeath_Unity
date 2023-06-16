using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using TMPro;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance { get { if (instance == null) instance = FindObjectOfType<InventoryManager>(); return instance; } }
    public static Item HeldItem { get; set; }
    [SerializeField] List<Item> itemsInventory = new List<Item>();
    [SerializeField] List<Recipe> recipesInventory = new List<Recipe>();
    [SerializeField] List<ResidentData> residentsInventory = new List<ResidentData>();
    [SerializeField] List<int> itemAmount = new List<int>();
    [SerializeField] CraftingManager craftingManager;
    [SerializeField] BookDisplayInventory bookDisplayInventory;
    [SerializeField] List<Item> itemToReset;

    public InventoryDatabase inventoryDatabase;

    public List<Item> ItemsInventory { get => itemsInventory; set => itemsInventory = value; }
    public List<Recipe> RecipesInventory { get => recipesInventory; set => recipesInventory = value; }
    public List<ResidentData> ResidentsInventory { get => residentsInventory; set => residentsInventory = value; }

    public event Action<Item> OnItemAdded;
    public event Action<Item> OnItemRemoved;

    [Header("Inventorys General Variables")]
    public Image inventoryImageItemToShow;
    public TextMeshProUGUI inventoryTextName;
    public TextMeshProUGUI inventoryTextDescription;
    
    [Header("Recipes General Variables")]
    public Image recipeImageIngredients_1;
    public TextMeshProUGUI recipeTextCurrentOwnIngredient_1;
    public TextMeshProUGUI recipeTextIngredientNeeded_1;
    public Image recipeImageIngredients_2;
    public TextMeshProUGUI recipeTextCurrentOwnIngredient_2;
    public TextMeshProUGUI recipeTextIngredientNeeded_2;
    public Image recipeImageIngredients_3;
    public TextMeshProUGUI recipeTextCurrentOwnIngredient_3;
    public TextMeshProUGUI recipeTextIngredientNeeded_3;
    public Image recipeImageItemToShow;
    public TextMeshProUGUI recipeTextName;
    public TextMeshProUGUI recipeTextDescription;

    [Header("Residents General Variables")]
    public TextMeshProUGUI residentTextDescription;
    public TextMeshProUGUI residentTextName;
    public Image imageResidentToShow;

    //#if !UNITY_EDITOR
    private void Start()
    {
        ResetIngredients();
    }
    //#endif

    private void OnEnable()
    {
        craftingManager.OnItemCraft += ItemCreated;
    }

    private void OnDisable()
    {
        craftingManager.OnItemCraft -= ItemCreated;
    }

    private void ItemCreated(CraftSetup selectedRecepie)
    {
        Item itemToCreate = selectedRecepie.ScriptableRecipe.item;
        Debug.Log(selectedRecepie.ScriptableRecipe.item + "have been create");

        PayForCraft(selectedRecepie);

        AddItem(itemToCreate);
    }

    private void PayForCraft(CraftSetup selectedRecepie)
    {
        bool havePayIngredient1 = false;
        bool havePayIngredient2 = false;
        bool havePayIngredient3 = false;

        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (!havePayIngredient1 && itemsInventory[i] == selectedRecepie.ScriptableRecipe.ingredient1.ingredientType)
            {
                itemsInventory[i].Amount -= selectedRecepie.ScriptableRecipe.ingredient1.IngredientAmount;
                CheckRemainingAmount(itemsInventory[i]);
                havePayIngredient1 = true;
            }
            else if (!havePayIngredient2 && itemsInventory[i] == selectedRecepie.ScriptableRecipe.ingredient2.ingredientType)
            {
                itemsInventory[i].Amount -= selectedRecepie.ScriptableRecipe.ingredient2.IngredientAmount;
                CheckRemainingAmount(itemsInventory[i]);
                havePayIngredient2 = true;
            }
            else if (!havePayIngredient3 && itemsInventory[i] == selectedRecepie.ScriptableRecipe.ingredient3.ingredientType)
            {
                itemsInventory[i].Amount -= selectedRecepie.ScriptableRecipe.ingredient3.IngredientAmount;
                CheckRemainingAmount(itemsInventory[i]);
                havePayIngredient3 = true;
            }

            if (havePayIngredient1 && havePayIngredient2 && havePayIngredient3)
            {
                craftingManager.RefreshIngredientsOwned();
                return;
            }
        }


    }

    public void AddItem(Item itemToAdd, int amount = 1)
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (itemsInventory[i] == itemToAdd)
            {
                itemsInventory[i].Amount += amount;
                RefreshItemAmount();
                return;
            }
        }

        OnItemAdded?.Invoke(itemToAdd);
        itemsInventory.Add(itemToAdd);
        itemsInventory[itemsInventory.Count - 1].Amount = amount;
        itemAmount.Add(0);
        RefreshItemAmount();
    }

    public void AddRecipe(Recipe recipe)
    {
        //Utilser les unlockedRecipe � la place
        recipesInventory.Add(recipe);
    }
    
    public void AddResident(ResidentData resident)
    {
        //Utilser les unlockedResidents � la place
        //residentsInventory.Add(resident);

        if (inventoryDatabase.unlockedResidents.Contains(resident))
            return;

        inventoryDatabase.unlockedResidents.Add(resident);

    }

    public void UI_AddItem(Item itemToAdd)
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (itemsInventory[i] == itemToAdd)
            {
                itemsInventory[i].Amount += 1;
                RefreshItemAmount();
                return;
            }
        }



        OnItemAdded?.Invoke(itemToAdd);
        itemsInventory.Add(itemToAdd);
        itemsInventory[itemsInventory.Count - 1].Amount = 1;
        itemAmount.Add(0);
        RefreshItemAmount();
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (itemsInventory[i] == itemToRemove)
            {
                itemsInventory[i].Amount--;

                if (itemsInventory[i].Amount <= 0)
                    itemsInventory.Remove(itemToRemove);

                RefreshItemAmount();
                bookDisplayInventory.RefreshInventorySlot();
                return;
            }
        }

        OnItemRemoved?.Invoke(itemToRemove);
        RefreshItemAmount();
    }

    public void CheckRemainingAmount(Item itemToRemove)
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (itemsInventory[i] == itemToRemove)
            {
                if (itemsInventory[i].Amount == 0)
                {
                    OnItemRemoved?.Invoke(itemToRemove);
                    itemsInventory.Remove(itemToRemove);
                }

                return;
            }
        }
    }

    public int GetIngredientAmount(Item itemToFindIn)
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            if (itemsInventory[i] == itemToFindIn)
            {
                return itemsInventory[i].Amount;
            }
        }

        return 0;
    }

    private void RefreshItemAmount()
    {
        for (int i = 0; i < itemsInventory.Count; i++)
        {
            itemAmount[i] = itemsInventory[i].Amount;
        }
    }

    public void ResetIngredients()
    {
        foreach (var item in itemToReset)
        {
            item.Amount = 0;
        }
    }

    /*public void AddResident2(ResidentData residentToAdd)
    {
        if (inventoryDatabase.unlockedResidents.Contains(residentToAdd))
            return;

        inventoryDatabase.unlockedResidents.Add(residentToAdd);
    }*/

}
