using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

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

public class CraftSetup : MonoBehaviour
{
    [SerializeField]
    ScriptableCraft ScriptableCraft;

    public SetupButton setupButton;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (ScriptableCraft == null)
            return;

        setupButton.my_image.sprite = ScriptableCraft.Sprite;
    }

    public void UI_ClickedOnMe()
    {
        DisplayInformations();
    }

    private void DisplayInformations()
    {
        if (ScriptableCraft == null)
            return;

        setupButton.middle_Image.sprite = ScriptableCraft.Sprite;
        setupButton.textIngredientNeeded_1.text = ScriptableCraft.IngredientAmount_1.ToString();
        setupButton.textIngredientNeeded_2.text = ScriptableCraft.IngredientAmount_2.ToString();
        setupButton.textIngredientNeeded_3.text = ScriptableCraft.IngredientAmount_3.ToString();
        setupButton.textDescription.text = ScriptableCraft.Description;
        setupButton.name.text = ScriptableCraft.Name;
    }
}
