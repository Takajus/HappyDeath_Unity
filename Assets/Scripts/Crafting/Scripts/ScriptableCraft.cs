using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Craft", menuName = "Scriptables/Craft", order = 2)]
public class ScriptableCraft : ScriptableObject
{
    public Sprite Sprite;
    public int IngredientAmount_1;
    public int IngredientAmount_2;
    public int IngredientAmount_3;

    public string Name;
    public string Description;
}
