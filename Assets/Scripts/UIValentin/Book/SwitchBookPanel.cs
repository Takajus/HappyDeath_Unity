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
        }

        //bookMarks[desired].GetComponent<Image>().color = selectedColor;
        panels[desired].SetActive(true);
    }
}