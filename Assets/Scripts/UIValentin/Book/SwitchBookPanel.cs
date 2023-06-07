using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class SwitchBookPanel : MonoBehaviour
{
    [SerializeField]
    GameObject[] panels;
    [SerializeField]
    GameObject[] bookMarks;

    [SerializeField]
    BookDisplayInventory bookDisplayInventory;
    [SerializeField]
    BookDisplayRecipes bookDisplayRecipes;

    [SerializeField]
    Color32 selectedColor;
    [SerializeField]
    Color32 baseColor;

    public void UI_DisplayPanel(int desired)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
            bookMarks[i].GetComponent<Image>().color = baseColor;
        }

        //bookMarks[desired].GetComponent<Image>().color = selectedColor;
        panels[desired].SetActive(true);
    }

    public void UI_SetBookMarkPosition(int targetedBookmark)
    {
        foreach (var bookmark in bookMarks)
        {
            Vector3 vector3Reset = new Vector3(-100, bookmark.transform.localPosition.y, bookmark.transform.localPosition.z);
            bookmark.GetComponent<RectTransform>().localPosition = vector3Reset;
            Debug.Log("Reset " + targetedBookmark + " " + vector3Reset);
        }
        Vector3 vector3Apply = new Vector3(-50, bookMarks[targetedBookmark].transform.localPosition.y, bookMarks[targetedBookmark].transform.localPosition.z);
        bookMarks[targetedBookmark].GetComponent<RectTransform>().localPosition = vector3Apply;
        Debug.Log("Selected =  " + targetedBookmark + " : " + vector3Apply);
    }
}
