using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryDatabase))]
public class InventoryDatabaseEditor : Editor
{
    static string pathItem = "Assets/Scriptables/Items/";
    static string pathRecipe = "Assets/Scriptables/Recipes/";
    static string pathDatabase = "Assets/Scriptables/InventoryDatabase.asset";
    InventoryDatabase source;


    [MenuItem("Assets/Create/Scriptables/Items")] //Permet d'ajouter un menu dans le clique droit
    static void CreateItem()
    {
        Item baseItem = (Item)CreateInstance("Item"); //Utilser cette fonction pour un scriptableobject
        string[] paths = new string[1]; //il fait chier il veut une liste
        paths[0] = pathItem; //voilà ton item de la liste
        string name = "Item_" + AssetDatabase.FindAssets("", paths).Length + ".asset"; //pas oublier le .asset
        AssetDatabase.CreateAsset(baseItem, pathItem + name); //Creer l'object avec le nom donner au chemin renseigné
        InventoryDatabase database = (InventoryDatabase)AssetDatabase.LoadAssetAtPath(pathDatabase, typeof(InventoryDatabase)); // Load la database
        database.items.Add(baseItem); //ajouter l'objet à la database
        Selection.activeObject = baseItem; //Permet de selectionner directement le dossier où a été créeer l'item
    }

    [MenuItem("Assets/Create/Scriptables/Recipe")] //Permet d'ajouter un menu dans le clique droit
    static void CreateCraft()
    {
        Recipe baseRecipe = (Recipe)CreateInstance("Recipe"); //Utilser cette fonction pour un scriptableobject
        string[] paths = new string[1]; //il fait chier il veut une liste
        paths[0] = pathRecipe; //voilà ton chemin
        string name = "Recipe_" + AssetDatabase.FindAssets("", paths).Length + ".asset"; //pas oublier le .asset
        AssetDatabase.CreateAsset(baseRecipe, pathRecipe + name); //Creer l'object avec le nom donner au chemin renseigné
        InventoryDatabase database = (InventoryDatabase)AssetDatabase.LoadAssetAtPath(pathDatabase, typeof(InventoryDatabase)); // Load la database
        database.allRecipes.Add(baseRecipe); //ajouter l'objet à la database
        Selection.activeObject = baseRecipe; //Permet de selectionner directement le dossier où a été créeer l'item
    }

    public override void OnInspectorGUI()
    {
        if (GUILayout.Button("Fetch"))
        {
            FetchItemsAndRecepies();
        }
        base.OnInspectorGUI();
    }
    private void FetchItemsAndRecepies()
    {
        source = target as InventoryDatabase;

        string[] paths = new string[2];
        paths[0] = pathItem;
        paths[1] = pathRecipe;


        source.items.Clear();
        source.allRecipes.Clear();
        foreach (var GUID in AssetDatabase.FindAssets("", paths))
        {
            foreach (var assetFind in AssetDatabase.LoadAllAssetsAtPath(AssetDatabase.GUIDToAssetPath(GUID)))
            {
                Debug.Log($"{assetFind}");
                if (assetFind as Item)
                {
                    source.items.Add((Item)assetFind);
                }
                else if (assetFind as Recipe)
                {
                    source.allRecipes.Add((Recipe)assetFind);
                }
            }

        }
    }
}
