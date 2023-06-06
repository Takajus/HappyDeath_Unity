using System.Collections;
using System.Collections.Generic;
using UnityEditor.Profiling;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Database", menuName = "Scriptables/InventoryDatabase", order = 0)]
public class InventoryDatabase : ScriptableObject
{
    [Header("Items")]
    public List<Item> items;
    [Header("Recipes")]
    public List<Recipe> unlockedRecipes;
    public List<Recipe> allRecipes;
    [Header("Residents")]
    public List<Item> unlockedResidents;
    public List<Item> allResidents;
}
