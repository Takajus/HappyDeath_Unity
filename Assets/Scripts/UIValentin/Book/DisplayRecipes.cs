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

    [SerializeField]
    ScriptableCraft ScriptableCraft;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (ScriptableCraft == null)
            return;

        setupButton.buttonImage.sprite = ScriptableCraft.Sprite;
    }
    public void UI_ClickedOnMe()
    {
        DisplayInformations();
    }

    private void DisplayInformations()
    {
        if (ScriptableCraft == null)
            return;

        setupButton.item.sprite = ScriptableCraft.Sprite;
        setupButton.textIngredientNeeded_1.text = ScriptableCraft.IngredientAmount_1.ToString();
        setupButton.textIngredientNeeded_2.text = ScriptableCraft.IngredientAmount_2.ToString();
        setupButton.textIngredientNeeded_3.text = ScriptableCraft.IngredientAmount_3.ToString();
        setupButton.textDescription.text = ScriptableCraft.Description;
        setupButton.name.text = ScriptableCraft.Name;
    }
}
