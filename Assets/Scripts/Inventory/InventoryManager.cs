using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryManager : MonoBehaviour
{
    private static InventoryManager instance;
    public static InventoryManager Instance { get { if (instance == null) instance = FindObjectOfType<InventoryManager>(); return instance; } }
    public static Item HeldItem { get; set; }
    [SerializeField] List<Item> inventory = new List<Item>();
    [SerializeField] List<int> itemAmount = new List<int>();
    [SerializeField] CraftingManager craftingManager;
    [SerializeField] BookDisplayInventory bookDisplayInventory;
    [SerializeField] List<Item> itemToReset;

    public InventoryDatabase inventoryDatabase;

    public List<Item> Inventory { get => inventory; set => inventory = value; }

    public event Action<Item> OnItemAdded;
    public event Action<Item> OnItemRemoved;

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

        for (int i = 0; i < inventory.Count; i++)
        {
            if (!havePayIngredient1 && inventory[i] == selectedRecepie.ScriptableRecipe.ingredient1.ingredientType)
            {
                inventory[i].Amount -= selectedRecepie.ScriptableRecipe.ingredient1.IngredientAmount;
                CheckRemainingAmount(inventory[i]);
                havePayIngredient1 = true;
            }
            else if (!havePayIngredient2 && inventory[i] == selectedRecepie.ScriptableRecipe.ingredient2.ingredientType)
            {
                inventory[i].Amount -= selectedRecepie.ScriptableRecipe.ingredient2.IngredientAmount;
                CheckRemainingAmount(inventory[i]);
                havePayIngredient2 = true;
            }
            else if (!havePayIngredient3 && inventory[i] == selectedRecepie.ScriptableRecipe.ingredient3.ingredientType)
            {
                inventory[i].Amount -= selectedRecepie.ScriptableRecipe.ingredient3.IngredientAmount;
                CheckRemainingAmount(inventory[i]);
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
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemToAdd)
            {
                inventory[i].Amount += amount;
                RefreshItemAmount();
                return;
            }
        }



        OnItemAdded?.Invoke(itemToAdd);
        inventory.Add(itemToAdd);
        inventory[inventory.Count - 1].Amount = amount;
        itemAmount.Add(0);
        RefreshItemAmount();
    }
    
    public void UI_AddItem(Item itemToAdd)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemToAdd)
            {
                inventory[i].Amount += 1;
                RefreshItemAmount();
                return;
            }
        }



        OnItemAdded?.Invoke(itemToAdd);
        inventory.Add(itemToAdd);
        inventory[inventory.Count - 1].Amount = 1;
        itemAmount.Add(0);
        RefreshItemAmount();
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemToRemove)
            {
                inventory[i].Amount--;

                if (inventory[i].Amount <= 0)
                    inventory.Remove(itemToRemove);

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
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemToRemove)
            {
                if (inventory[i].Amount == 0)
                {
                    OnItemRemoved?.Invoke(itemToRemove);
                    inventory.Remove(itemToRemove);
                }

                return;
            }
        }
    }

    public int GetIngredientAmount(Item itemToFindIn)
    {
        for (int i = 0; i < Inventory.Count; i++)
        {
            if (Inventory[i] == itemToFindIn)
            {
                return Inventory[i].Amount;
            }
        }

        return 0;
    }

    private void RefreshItemAmount()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            itemAmount[i] = inventory[i].Amount;
        }
    }

    public void UICheat_AddItem(Item itemToAdd)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemToAdd)
            {
                inventory[i].Amount += 100;
                RefreshItemAmount();
                return;
            }
        }

        OnItemAdded?.Invoke(itemToAdd);
        inventory.Add(itemToAdd);
        itemAmount.Add(0);
        RefreshItemAmount();
    }

    public void ResetIngredients()
    {
        foreach (var item in itemToReset)
        {
            item.Amount = 0;
        }
    }

}
