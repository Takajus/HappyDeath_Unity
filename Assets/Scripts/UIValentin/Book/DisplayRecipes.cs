using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;



public class DisplayRecipes : MonoBehaviour
{
    [System.Serializable]
    public struct SetupButton
    {
        public Image buttonImage;
        public Image background;
        public Image item;
        public Image icon;
        public TextMeshProUGUI name;
        [Header("Ingredients")]
        public Image imageIngredients_1;
        public TextMeshProUGUI textActualOwnIngredient_1;
        public TextMeshProUGUI textIngredientNeeded_1;
        public Image imageIngredients_2;
        public TextMeshProUGUI textActualOwnIngredient_2;
        public TextMeshProUGUI textIngredientNeeded_2;
        public Image imageIngredients_3;
        public TextMeshProUGUI textActualOwnIngredient_3;
        public TextMeshProUGUI textIngredientNeeded_3;
        [Space]
        public TextMeshProUGUI textDescription;
    }

    public SetupButton setupButton;

    //[HideInInspector]
    public Recipe scriptableRecipe;

    private void Start()
    {
        Refresh();
    }
    public void SetScriptableRecipe(Recipe givenRecipe)
    {
        scriptableRecipe = givenRecipe;
    }

    public void Refresh()
    {
        if (scriptableRecipe == null)
            return;

        setupButton.icon.color = Color.white;
        setupButton.icon.sprite = scriptableRecipe.Sprite;
    }
    public void UI_ClickedOnMe()
    {
        DisplayInformations();
    }

    public void DisplayInformations()
    {
        if (scriptableRecipe == null)
        {
            setupButton.icon.color = new Color(0, 0, 0, 0);
            return;
        }

        setupButton.icon.color = Color.white;
        setupButton.item.sprite = scriptableRecipe.Sprite;
        setupButton.textIngredientNeeded_2.text = scriptableRecipe.ingredient2.IngredientAmount.ToString();
        setupButton.textIngredientNeeded_3.text = scriptableRecipe.ingredient3.IngredientAmount.ToString();
        setupButton.textDescription.text = scriptableRecipe.Description;
        setupButton.name.text = scriptableRecipe.Name;
    }
}
