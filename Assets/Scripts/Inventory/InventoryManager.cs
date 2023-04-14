using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static InventoryManager Instance;

    [SerializeField] List<Item> inventory;

    public List<Item> Inventory { get => inventory; set => inventory = value; }

    public event Action OnItemAdded;
    public event Action OnItemRemoved;

    public void Awake()
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

    public void AddItem(Item itemToAdd)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemToAdd)
            {
                inventory[i].Amount++;
                return;
            }           
        }

        OnItemAdded?.Invoke();
        inventory.Add(itemToAdd);
    }

    public void RemoveItem(Item itemToRemove)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i] == itemToRemove)
            {
                inventory[i].Amount--;

                if (inventory[i].Amount == 0)
                {
                    OnItemRemoved.Invoke();
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

}
