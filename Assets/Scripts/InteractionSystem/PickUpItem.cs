using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUpItem : MonoBehaviour
{
    bool canPickUp = false;
    [SerializeField] GameObject E_Input;
    [SerializeField] InputActionReference pickUpAction;
    [SerializeField] Item resourceToPick;

    private void OnEnable()
    {
        pickUpAction.action.performed += PickUp;
    }

    private void OnDisable()
    {
        pickUpAction.action.performed -= PickUp;
    }

    private void OnTriggerEnter(Collider other)
    {
        canPickUp = true;
        E_Input.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        canPickUp = false;
        E_Input.SetActive(false);
    }

    private void PickUp(InputAction.CallbackContext context)
    {
        if (!canPickUp)
            return;

        gameObject.SetActive(false);
        InventoryManager.Instance.AddItem(resourceToPick);
        Destroy(gameObject);
    }
}
