using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class CraftSetup : MonoBehaviour
{
    [System.Serializable]
    public struct SetupButton
    {
        public Image my_image;
        public Image middle_Image;
        public TextMeshProUGUI textIngredientNeeded_1;
        public TextMeshProUGUI textIngredientNeeded_2;
        public TextMeshProUGUI textIngredientNeeded_3;
        public TextMeshProUGUI textDescription;
        public TextMeshProUGUI name;
    }

    [SerializeField]
    private Recipe scriptableRecipe;

    public SetupButton setupButton;

    public Recipe ScriptableRecipe { get => scriptableRecipe; set => scriptableRecipe = value; }

    public void SetScriptableRecipe(Recipe givenRecipe)
    {
        scriptableRecipe = givenRecipe;
    }

    public void Refresh()
    {
        if (scriptableRecipe == null)
        {
            setupButton.my_image.color = new Color(0,0,0,0);
            return;
        }

        setupButton.my_image.color = Color.white;
        setupButton.my_image.sprite = scriptableRecipe.Sprite;
    }

    public void UI_ClickedOnMe()
    {
        CraftingManager.Instance.OnRecipeSelected.Invoke(this);
        DisplayInformations();
    }

    public void DisplayInformations()
    {
        if (scriptableRecipe == null)
            return;

        setupButton.middle_Image.sprite = scriptableRecipe.Sprite;
        setupButton.textIngredientNeeded_1.text = scriptableRecipe.ingredient1.IngredientAmount.ToString();
        setupButton.textIngredientNeeded_2.text = scriptableRecipe.ingredient2.IngredientAmount.ToString();
        setupButton.textIngredientNeeded_3.text = scriptableRecipe.ingredient3.IngredientAmount.ToString();
        setupButton.textDescription.text = scriptableRecipe.Description;
        setupButton.name.text = scriptableRecipe.Name;
    }
}
