using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DisplayResidentStock : MonoBehaviour
{
    public static bool IsOpen;

    [SerializeField] GameObject panel;
    [SerializeField] GameObject slotPrefab;
    [SerializeField] RectTransform topLeftOrigin;
    [SerializeField] RectTransform bottomLeftOrigin;
    [SerializeField] List<GameObject> residentSlots;
    [SerializeField] List<ResidentData> residentList;
    [SerializeField] ResidentData selectedResident;

    [SerializeField] Image residentSprite;
    [SerializeField] TextMeshProUGUI dislikesText;
    [SerializeField] TextMeshProUGUI likesText;
    [SerializeField] TextMeshProUGUI nameAndRaceText;
    [SerializeField] GameObject noUnassignedResidentText;

    [SerializeField] float widthSlotNumber;
    [SerializeField] float heigthSlotNumber;

    private void Start()
    {
        ToggleDisplay(false);
    }

    public void ToggleDisplay(bool status)
    {
        IsOpen = status;

        panel.SetActive(IsOpen);

        if (IsOpen)
            InitializePanel();
    }

    void InitializePanel()
    {
        residentList = InventoryManager.Instance.inventoryDatabase.unlockedResidents.Where(e => !e.isAssign).ToList();

        for (int i = 0; i < residentSlots.Count; i++)
        {
            Destroy(residentSlots[i]);
        }
        residentSlots.Clear();

        if (residentList.Count <= 0)
        {
            noUnassignedResidentText.SetActive(true);
            residentSprite.enabled = false;
            dislikesText.enabled = false;
            likesText.enabled = false;
            return;
        }
        else
        {
            noUnassignedResidentText.SetActive(false);
            residentSprite.enabled = true;
            dislikesText.enabled = true;
            likesText.enabled = true;
        }

        float width = (bottomLeftOrigin.anchoredPosition.x - topLeftOrigin.anchoredPosition.x) / (widthSlotNumber - 1);
        float height = (bottomLeftOrigin.anchoredPosition.y - topLeftOrigin.anchoredPosition.y) / (heigthSlotNumber - 1);


        for (int i = 0; i < residentList.Count; i++)
        {
            if (i >= residentSlots.Count)
            {
                float posX = topLeftOrigin.anchoredPosition.x + (width * (i % widthSlotNumber));
                float posY = topLeftOrigin.anchoredPosition.y + (height * Mathf.FloorToInt(i / widthSlotNumber));

                GameObject newSlot = Instantiate(slotPrefab, topLeftOrigin.transform.position, Quaternion.identity, panel.transform);
                newSlot.GetComponent<RectTransform>().anchoredPosition = new Vector2(posX, posY);
                residentSlots.Add(newSlot);
            }

            int id = i;
            residentSlots[i].GetComponent<Image>().sprite = residentList[i].sprite;
            residentSlots[i].GetComponent<Button>().onClick.AddListener(() =>
            {
                SelectResident(residentList[id]);
            });
        }

        SelectResident(residentList[0]);
    }

    void SelectResident(ResidentData resident)
    {
        selectedResident = resident;
        residentSprite.sprite = selectedResident.sprite;
        nameAndRaceText.text = selectedResident.name + ", " + selectedResident.race + ", " + selectedResident.role;

        string label = "Likes :\n\n";
        foreach (var like in selectedResident.elementList.Where(e => e.likeDislike == LikeDislike.Like))
        {
            switch (like.preferenceType)
            {
                case Category.Object:
                    label += like.objectLike + "\n";
                    break;
                case Category.Social:
                    label += like.race + "\n";
                    break;
                case Category.Place:
                    label += like.objectLike + "\n";
                    break;
            }
        }
        likesText.text = label;

        label = "Dislikes :\n\n";
        foreach (var like in selectedResident.elementList.Where(e => e.likeDislike == LikeDislike.Dislike))
        {
            switch (like.preferenceType)
            {
                case Category.Object:
                    label += like.objectLike + "\n";
                    break;
                case Category.Social:
                    label += like.race + "\n";
                    break;
                case Category.Place:
                    label += like.objectLike + "\n";
                    break;
            }
        }
        dislikesText.text = label;
    }

    public void ExtractSoul()
    {
        if (selectedResident == null)
            return;

        InteractHandler.transportedResident = selectedResident;
        ToggleDisplay(false);
    }
}
