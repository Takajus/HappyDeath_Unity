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
    public Recipe scriptableRecipe;

    [SerializeField] Image imageIngredient_1;
    [SerializeField] Image imageIngredient_2;
    [SerializeField] Image imageIngredient_3;

    [SerializeField] TextMeshProUGUI ingredientAmountOwned_1;
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_2;
    [SerializeField] TextMeshProUGUI ingredientAmountOwned_3;

    [SerializeField] GameObject parent_1;
    [SerializeField] GameObject parent_2;
    [SerializeField] GameObject parent_3;

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

        if (scriptableRecipe.ingredient1.IngredientAmount > 0)
        {
            parent_1.SetActive(true);
            imageIngredient_1.sprite = scriptableRecipe.ingredient1.ingredientType.Sprite;
            imageIngredient_1.color = Color.white;
            setupButton.textIngredientNeeded_1.text = scriptableRecipe.ingredient1.IngredientAmount.ToString();
            ingredientAmountOwned_1.text = HUDManager.GetInventoryManager().GetIngredientAmount(scriptableRecipe.ingredient1.ingredientType).ToString();
        }
        else
        {
            imageIngredient_1.color = new Color(0, 0, 0, 0);
            parent_1.SetActive(false);
        }

        if (scriptableRecipe.ingredient2.IngredientAmount > 0)
        {
            parent_2.SetActive(true);
            imageIngredient_2.sprite = scriptableRecipe.ingredient2.ingredientType.Sprite;
            imageIngredient_2.color = Color.white;
            setupButton.textIngredientNeeded_2.text = scriptableRecipe.ingredient2.IngredientAmount.ToString();
            ingredientAmountOwned_2.text = HUDManager.GetInventoryManager().GetIngredientAmount(scriptableRecipe.ingredient2.ingredientType).ToString();
        }
        else
        {
            imageIngredient_2.color = new Color(0, 0, 0, 0);
            parent_2.SetActive(false);
        }

        if (scriptableRecipe.ingredient3.IngredientAmount > 0)
        {
            parent_3.SetActive(true);
            imageIngredient_3.sprite = scriptableRecipe.ingredient3.ingredientType.Sprite;
            imageIngredient_3.color = Color.white;
            setupButton.textIngredientNeeded_3.text = scriptableRecipe.ingredient3.IngredientAmount.ToString();
            ingredientAmountOwned_3.text = HUDManager.GetInventoryManager().GetIngredientAmount(scriptableRecipe.ingredient3.ingredientType).ToString();
        }
        else
        {
            imageIngredient_3.color = new Color(0, 0, 0, 0);
            parent_3.SetActive(false);
        }

        setupButton.textDescription.text = scriptableRecipe.Description;
        setupButton.name.text = scriptableRecipe.Name;
    }
}
