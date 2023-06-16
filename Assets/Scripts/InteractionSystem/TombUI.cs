using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TombUI : MonoBehaviour
{
    public static bool IsOpen;

    private Tomb currentTomb;
    [SerializeField] GameObject nothingAssigned;

    [Header("Extract")]
    [SerializeField] GameObject extractParent;
    [SerializeField] Image extractImage;
    [SerializeField] TextMeshProUGUI extractText;
    [SerializeField] Button extractButton;

    [Header("Assign")]
    [SerializeField] GameObject assignParent;
    [SerializeField] Image assignImage;
    [SerializeField] TextMeshProUGUI assignText;
    [SerializeField] Button assignButton;

    public void ToggleDisplay(bool status, Tomb tomb = null)
    {
        IsOpen = status;
        currentTomb = tomb;

        gameObject.SetActive(IsOpen);

        if (IsOpen)
            InitializePanel();
    }

    void InitializePanel()
    {
        if (InteractHandler.transportedResident)
        {
            assignParent.SetActive(true);
            assignImage.sprite = InteractHandler.transportedResident.sprite;
            assignText.text = InteractHandler.transportedResident.residentName;
            assignButton.onClick.RemoveAllListeners();
            assignButton.onClick.AddListener(() =>
            {
                currentTomb.AssignNPC(InteractHandler.transportedResident);
                InitializePanel();
            });
        }
        else
            assignParent.SetActive(false);

        if (currentTomb.residentData)
        {
            extractParent.SetActive(true);
            extractImage.sprite = currentTomb.residentData.sprite;
            extractText.text = currentTomb.residentData.residentName;
            extractButton.onClick.RemoveAllListeners();
            extractButton.onClick.AddListener(() =>
            {
                currentTomb.ExtractNPC();
                InitializePanel();
            });
        }
        else
            extractParent.SetActive(false);

        if (InteractHandler.transportedResident == null && currentTomb.residentData == null)
            nothingAssigned.SetActive(true);
        else
            nothingAssigned.SetActive(false);
    }
}