using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonDisplayResidents : MonoBehaviour
{
    [System.Serializable]
    public struct SetupButton
    {
        public Image buttonImage;
        public Image background;
        public Image imageResidentToShow;
        public Image icon;
        public TextMeshProUGUI name;
        public TextMeshProUGUI description;
    }

    public SetupButton setupButton;
    public ResidentData scriptableResident;

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
        setupButton.description = InventoryManager.Instance.residentTextDescription;
        setupButton.name = InventoryManager.Instance.residentTextName;
        setupButton.imageResidentToShow = InventoryManager.Instance.imageResidentToShow;
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

    public void UI_Hover()
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

        if (setupButton.description == null)
        {
            return;
        }

        setupButton.icon.color = Color.white;
        setupButton.imageResidentToShow.color = Color.white;
        setupButton.imageResidentToShow.sprite = scriptableResident.sprite;
        setupButton.description.text = scriptableResident.description;
        setupButton.name.text = scriptableResident.residentName;
    }
}
