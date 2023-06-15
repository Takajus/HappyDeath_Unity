using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShortcutWheel : MonoBehaviour
{
    float degree;
    int offset = 90;
    public int distance = 180;
    public int distanceLine = 125;
    public GameObject toolWheel;
    public GameObject buildsWheel;
    public List<GameObject> toolsElements = new List<GameObject>();
    public List<GameObject> buildsElements = new List<GameObject>();
    public List<GameObject> lineNbrToolsElement = new List<GameObject>();
    public List<GameObject> lineNbrBuildsElement = new List<GameObject>();
    public GameObject toolWheelPrefab;
    public GameObject linePrefab;

    private void Start()
    {
        InputManager.Instance.uiWheelShortcutAction.action.performed += DisplayWheel;
        InputManager.Instance.uiWheelShortcutAction.action.canceled += CloseWheel;
        InventoryManager.Instance.OnItemAdded += OnItemAdded;
        InventoryManager.Instance.OnItemRemoved += OnItemRemoved;
    }

    private void OnItemRemoved(Item item)
    {
        for (int i = 0; i < buildsElements.Count; i++)
        {
            if (buildsElements[i].TryGetComponent<ToolWheel>(out ToolWheel toolwheel))
            {
                if (toolwheel.item == item)
                {
                    Destroy(buildsElements[i].gameObject);
                    Destroy(lineNbrBuildsElement[0].gameObject);
                }
            }

        }
    }

    public void OnItemAdded(Item item)
    {
        if (item == null || item.itemType == Item.ItemType.RESOURCE)
            return;

        GameObject tempWheelObject = new GameObject();
        GameObject line = new GameObject();

        if (item.itemType == Item.ItemType.TOOL)
        {
            tempWheelObject = Instantiate(toolWheelPrefab, toolWheel.transform);
            line = Instantiate(linePrefab, toolWheel.transform);
            toolsElements.Insert(toolsElements.Count - 1, tempWheelObject);
            lineNbrToolsElement.Add(line);
        }
        else if (item.itemType == Item.ItemType.BUILD)
        {
            tempWheelObject = Instantiate(toolWheelPrefab, buildsWheel.transform);
            line = Instantiate(linePrefab, buildsWheel.transform);
            buildsElements.Insert(buildsElements.Count - 1, tempWheelObject);
            lineNbrBuildsElement.Add(line);
        }


        tempWheelObject.GetComponent<ToolWheel>().item = item;
        tempWheelObject.GetComponent<Image>().sprite = item.Sprite;
        tempWheelObject.GetComponent<Button>().onClick.AddListener(() =>
        {
            //CloseWheel(new InputAction.CallbackContext());
            //InventoryManager.HeldItem = item;
            //tempWheelObject.transform.parent.gameObject.SetActive(false);

            EquipItem(item);
        });
    }

    public void EquipItem(Item item)
    {
        CloseWheel(new InputAction.CallbackContext());
        InventoryManager.HeldItem = item;
    }

    public void DisplayWheel(InputAction.CallbackContext context)
    {
        UI_ActivateWheel();
        degree = 360 / toolsElements.Count;

        for (int i = 0; i < toolsElements.Count; i++)
        {
            Vector2 position = new Vector2(Mathf.Cos((degree * i + offset) * Mathf.Deg2Rad),Mathf.Sin((degree * i +offset) * Mathf.Deg2Rad));
            toolsElements[i].GetComponent<RectTransform>().localPosition = position * distance;
            Vector2 linePosition = new Vector2(Mathf.Cos((degree * i + offset + degree / 2) * Mathf.Deg2Rad),Mathf.Sin((degree * i + offset + degree / 2) * Mathf.Deg2Rad));
            lineNbrToolsElement[i].GetComponent<RectTransform>().localPosition = linePosition * distanceLine;
            lineNbrToolsElement[i].GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, degree * i + offset + degree / 2);
        }
    }

    //Called by BuildsWheelButton
    public void UI_DisplayBuildsWheel()
    {
        OpenBuildsWheel();

        if (buildsElements.Count == 0)
        {
            return;
        }

        degree = 360 / buildsElements.Count;

        for (int i = 0; i < buildsElements.Count; i++)
        {
            Vector2 position = new Vector2(Mathf.Cos((degree * i + offset) * Mathf.Deg2Rad), Mathf.Sin((degree * i + offset) * Mathf.Deg2Rad));
            buildsElements[i].GetComponent<RectTransform>().localPosition = position * distance;
            Vector2 linePosition = new Vector2(Mathf.Cos((degree * i + offset + degree / 2) * Mathf.Deg2Rad), Mathf.Sin((degree * i + offset + degree / 2) * Mathf.Deg2Rad));
            lineNbrBuildsElement[i].GetComponent<RectTransform>().localPosition = linePosition * distanceLine;
            lineNbrBuildsElement[i].GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, degree * i + offset + degree / 2);
        }
    }

    private void OpenBuildsWheel()
    {
        toolWheel.SetActive(false);
        buildsWheel.SetActive(true);
    }

    public void UI_ActivateWheel()
    {
        toolWheel.SetActive(true);
        buildsWheel.SetActive(false);
    }

    private void CloseWheel(InputAction.CallbackContext context)
    {
        buildsWheel.SetActive(false);
        toolWheel.SetActive(false);
    }

    public void CloseWheel()
    {
        buildsWheel.SetActive(false);
        toolWheel.SetActive(false);
    }
}
