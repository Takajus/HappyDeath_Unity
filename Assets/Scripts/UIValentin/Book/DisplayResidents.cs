using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResidents : MonoBehaviour
{
    [System.Serializable]
    public struct SetupButton
    {
        public Image buttonImage;
        public Image background;
        public Image resident;
        public TextMeshProUGUI name;
        public TextMeshProUGUI description;
    }

    public SetupButton setupButton;
    public ResidentSample scriptableResident;

    private void Start()
    {
        Refresh();
    }

    public void SetScriptableRecipe(ResidentSample givenResident)
    {
        scriptableResident = givenResident;
    }

    public void Refresh()
    {
        if (scriptableResident == null)
            return;

        setupButton.buttonImage.sprite = scriptableResident.sprite;
    }

    public void UI_ClickedOnMe()
    {
        DisplayInformations();
    }

    public void DisplayInformations()
    {
        if (scriptableResident == null)
            return;

        setupButton.resident.sprite = scriptableResident.sprite;
        setupButton.description.text = scriptableResident.description;
        setupButton.name.text = scriptableResident.residentName;
    }
}
