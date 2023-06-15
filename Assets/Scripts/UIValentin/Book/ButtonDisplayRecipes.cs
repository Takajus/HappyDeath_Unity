using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class ButtonDisplayRecipes : MonoBehaviour
{
    [System.Serializable]
    public struct SetupButton
    {
        public Image buttonImage;
        public Image background;
        public Image imageItemToShow;
        public Image icon;
        public TextMeshProUGUI textName;
        [Header("Ingredients")]
        public Image imageIngredients_1;
        public TextMeshProUGUI textCurrentOwnIngredient_1;
        public TextMeshProUGUI textIngredientNeeded_1;
        public Image imageIngredients_2;
        public TextMeshProUGUI textCurrentOwnIngredient_2;
        public TextMeshProUGUI textIngredientNeeded_2;
        public Image imageIngredients_3;
        public TextMeshProUGUI textCurrentOwnIngredient_3;
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
        GetVariables();

        EventTrigger.Entry entry = new EventTrigger.Entry();
        entry.eventID = EventTriggerType.PointerEnter;
        entry.callback.AddListener((eventData) => { UI_Hover(); });
        GetComponent<EventTrigger>().triggers.Add(entry);

        DisplayInformations();
    }

    private void GetVariables()
    {
        setupButton.imageIngredients_1 = InventoryManager.Instance.recipeImageIngredients_1;
        setupButton.textCurrentOwnIngredient_1 = InventoryManager.Instance.recipeTextCurrentOwnIngredient_1;
        setupButton.textIngredientNeeded_1 = InventoryManager.Instance.recipeTextIngredientNeeded_1;

        setupButton.imageIngredients_2 = InventoryManager.Instance.recipeImageIngredients_2;
        setupButton.textCurrentOwnIngredient_2 = InventoryManager.Instance.recipeTextCurrentOwnIngredient_2;
        setupButton.textIngredientNeeded_2 = InventoryManager.Instance.recipeTextIngredientNeeded_2;

        setupButton.imageIngredients_3 = InventoryManager.Instance.recipeImageIngredients_3;
        setupButton.textCurrentOwnIngredient_3 = InventoryManager.Instance.recipeTextCurrentOwnIngredient_3;
        setupButton.textIngredientNeeded_3 = InventoryManager.Instance.recipeTextIngredientNeeded_3;

        setupButton.textDescription = InventoryManager.Instance.recipeTextDescription;
        setupButton.textName = InventoryManager.Instance.recipeTextName;
        setupButton.imageItemToShow = InventoryManager.Instance.recipeImageItemToShow;
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

    public void UI_Hover()
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

        if (setupButton.imageItemToShow == null)
        {
            return;
        }

        setupButton.icon.color = Color.white;
        setupButton.icon.sprite = scriptableRecipe.Sprite;
        setupButton.imageItemToShow.sprite = scriptableRecipe.Sprite;
        setupButton.textIngredientNeeded_2.text = scriptableRecipe.ingredient2.IngredientAmount.ToString();
        setupButton.textIngredientNeeded_3.text = scriptableRecipe.ingredient3.IngredientAmount.ToString();
        setupButton.textDescription.text = scriptableRecipe.Description;
        setupButton.textName.text = scriptableRecipe.Name;
    }
}
