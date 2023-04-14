using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Craft", menuName = "Scriptables/Craft", order = 2)]
public class ScriptableCraft : ScriptableObject
{
    [System.Serializable]
    public struct Ingredient
    {
        public Item ingredientType;
        public int IngredientAmount;
    }

    public Sprite Sprite;
    public Ingredient ingredient1;
    public Ingredient ingredient2;
    public Ingredient ingredient3;
    public Item item;

    public string Name;
    public string Description;
}
