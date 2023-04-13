using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SwitchBookPanel : MonoBehaviour
{
    [SerializeField]
    GameObject[] panels;
    [SerializeField]
    GameObject[] bookMarks;

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

        bookMarks[desired].GetComponent<Image>().color = selectedColor;
        panels[desired].SetActive(true);
    }

}
