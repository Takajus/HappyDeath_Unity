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
        public Image icon;
        public TextMeshProUGUI name;
        public TextMeshProUGUI description;
    }

    public SetupButton setupButton;
    public ResidentData scriptableResident;

    private void Start()
    {
        Refresh();
    }

    public void SetScriptableRecipe(ResidentData givenResident)
    {
        scriptableResident = givenResident;
    }

    public void Refresh()
    {
        if (scriptableResident == null)
            return;

        setupButton.icon.color = Color.white;
        setupButton.icon.sprite = scriptableResident.sprite;
    }

    public void UI_ClickedOnMe()
    {
        DisplayInformations();
    }

    public void DisplayInformations()
    {
        if (scriptableResident == null)
        {
            setupButton.icon.color = new Color(0, 0, 0, 0);
            return;
        }

        setupButton.icon.color = Color.white;
        setupButton.resident.sprite = scriptableResident.sprite;
        setupButton.description.text = scriptableResident.description;
        setupButton.name.text = scriptableResident.residentName;
    }
}
