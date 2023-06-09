using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenu(fileName = "Database", menuName = "Scriptables/InventoryDatabase", order = 0)]
#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
public class InventoryDatabase : ScriptableObject
{
    [Header("Items")]
    public List<Item> items;

    [Header("Recipes")]
    public List<Recipe> unlockedRecipes;
    public List<Recipe> allRecipes;

    [Header("Residents")]
    public List<ResidentData> unlockedResidents;
    public List<ResidentData> allResidents;

    [Header("Starting kit")]
    [SerializeField] protected List<Item> startingItems;
    [SerializeField] protected List<Recipe> startingRecipes;
    [SerializeField] protected List<ResidentData> startingResidents;

    public void OnEnable()
    {
        unlockedResidents = new List<ResidentData>();
        unlockedRecipes.AddRange(startingRecipes);

        unlockedResidents = new List<ResidentData>();
        unlockedResidents.AddRange(startingResidents);

        foreach (var resident in allResidents)
        {
            resident.isAssign = false;
        }
    }
}
