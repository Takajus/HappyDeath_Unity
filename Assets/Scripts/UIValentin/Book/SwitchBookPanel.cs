using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System;

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

    [SerializeField] GameObject pageItemsPrefab;
    [SerializeField] GameObject pageRecipesPrefab;
    [SerializeField] GameObject pageResidentsPrefab;
    [SerializeField] float slotPerPage = 16;

    public void UI_DisplayPanel(int desired)
    {
        for (int i = 0; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panels[desired].SetActive(true);
    }

    public bool IsNewPageNeeded(float slotCount, int pagesCount)
    {
        int pageNumberNeeded = Mathf.CeilToInt(slotCount / slotPerPage);
        if (pagesCount < pageNumberNeeded)
        {
            return true;
        }
        return false;
    }

    public GameObject CreateItemsPage(GameObject parent)
    {
        return Instantiate(pageItemsPrefab, parent.transform);
    }

    public GameObject CreateRecipesPage(GameObject parent)
    {
        return Instantiate(pageRecipesPrefab, parent.transform);
    }

    public GameObject CreateResidentsPage(GameObject parent)
    {
        return Instantiate(pageResidentsPrefab, parent.transform);
    }
}