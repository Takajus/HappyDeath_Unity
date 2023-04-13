using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DisplayInventory : MonoBehaviour
{
    [System.Serializable]
    public struct SetupButton
    {
        public Image buttonImage;
        public Image item;
        public TextMeshProUGUI name;
        public TextMeshProUGUI textDescription;
    }

    public SetupButton setupButton;
    public Item item;

    private void Start()
    {
        Refresh();
    }

    public void Refresh()
    {
        if (item == null)
            return;

        setupButton.buttonImage.sprite = item.Sprite;
    }
    public void UI_ClickedOnMe()
    {
        DisplayInformations();
    }

    private void DisplayInformations()
    {
        if (item == null)
            return;

        setupButton.item.sprite = item.Sprite;
        setupButton.textDescription.text = item.Description;
        setupButton.name.text = item.Name;
    }
}

