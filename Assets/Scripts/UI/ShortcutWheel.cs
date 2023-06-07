using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShortcutWheel : MonoBehaviour
{
    float degree;
    int offset = 90;
    public int distance = 0;
    public GameObject wheelParent;
    public List<GameObject> nbrElement = new List<GameObject>();
    public GameObject toolWheelPrefab;

    private void Start()
    {
        InputManager.Instance.uiWheelShortcutAction.action.performed += DisplayWheel;
        InputManager.Instance.uiWheelShortcutAction.action.canceled += CloseWheel;
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
        }
    }

    private void CloseWheel(InputAction.CallbackContext context)
    {
        wheelParent.SetActive(false);
    }
}