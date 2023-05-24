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
    private Recipe scriptableCraft;

    public SetupButton setupButton;

    public Recipe ScriptableCraft { get => scriptableCraft; set => scriptableCraft = value; }

    public void Refresh()
    {
        if (scriptableCraft == null)
            return;

        setupButton.my_image.sprite = scriptableCraft.Sprite;
    }

    public void UI_ClickedOnMe()
    {
        CraftingManager.Instance.OnRecipeSelected.Invoke(this);
        DisplayInformations();
    }

    public void DisplayInformations()
    {
        if (scriptableCraft == null)
            return;

        setupButton.middle_Image.sprite = scriptableCraft.Sprite;
        setupButton.textIngredientNeeded_1.text = scriptableCraft.ingredient1.IngredientAmount.ToString();
        setupButton.textIngredientNeeded_2.text = scriptableCraft.ingredient2.IngredientAmount.ToString();
        setupButton.textIngredientNeeded_3.text = scriptableCraft.ingredient3.IngredientAmount.ToString();
        setupButton.textDescription.text = scriptableCraft.Description;
        setupButton.name.text = scriptableCraft.Name;
    }
}
