using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("hit " + other.gameObject.name);
    }
}
