using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ShortcutWheel : MonoBehaviour
{
    float degree;
    int offset = 90;
    public int distance = 180;
    public int distanceLine = 125;
    public GameObject wheelParent;
    public List<GameObject> nbrElement = new List<GameObject>();
    public List<GameObject> lineNbrElement = new List<GameObject>();
    public GameObject toolWheelPrefab;
    public GameObject toolSelected;
    public GameObject linePrefab;

    //public Action onToolCreated;

    private void Start()
    {
        InputManager.Instance.uiWheelShortcutAction.action.performed += DisplayWheel;
        InputManager.Instance.uiWheelShortcutAction.action.canceled += CloseWheel;
        InventoryManager.Instance.OnItemAdded += OnItemAdded;
    }

    public void OnItemAdded(Item item)
    {
        if (item.itemType == Item.ItemType.TOOL || item.itemType == Item.ItemType.BUILD)
        {
            GameObject tempToolWheel = Instantiate(toolWheelPrefab, wheelParent.transform);
            GameObject line = Instantiate(linePrefab, wheelParent.transform);
            tempToolWheel.GetComponent<ToolWheel>().item = item;
            tempToolWheel.GetComponent<Image>().sprite = item.Sprite;
            nbrElement.Insert(nbrElement.Count -1,tempToolWheel);
            lineNbrElement.Add(line);
            tempToolWheel.GetComponent<Button>().onClick.AddListener(() =>
            {
                InventoryManager.HeldItem = item;
                tempToolWheel.transform.parent.gameObject.SetActive(false);
            });
        }
    }

    public void AddToolWheel()
    {
        GameObject tempToolWheel = Instantiate(toolWheelPrefab, wheelParent.transform);
        nbrElement.Add(tempToolWheel);
    }

    public void DisplayWheel(InputAction.CallbackContext context)
    {
        wheelParent.SetActive(true);
        degree = 360 / nbrElement.Count;

        for (int i = 0; i < nbrElement.Count; i++)
        {
            Vector2 position = new Vector2(Mathf.Cos((degree * i + offset) * Mathf.Deg2Rad),Mathf.Sin((degree * i +offset) * Mathf.Deg2Rad));
            nbrElement[i].GetComponent<RectTransform>().localPosition = position * distance;
            Vector2 linePosition = new Vector2(Mathf.Cos((degree * i + offset + degree / 2) * Mathf.Deg2Rad),Mathf.Sin((degree * i + offset + degree / 2) * Mathf.Deg2Rad));
            lineNbrElement[i].GetComponent<RectTransform>().localPosition = linePosition * distanceLine;
            lineNbrElement[i].GetComponent<RectTransform>().localEulerAngles = new Vector3(0, 0, degree * i + offset + degree / 2);
        }
    }

    private void CloseWheel(InputAction.CallbackContext context)
    {
        wheelParent.SetActive(false);
    }
}
