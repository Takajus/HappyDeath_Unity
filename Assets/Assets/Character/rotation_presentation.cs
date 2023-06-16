using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation_presentation : MonoBehaviour
{
    public float vitesseRotation =1;

    // Update is called once per frame
    void Update()
    {
        var rotationVector = transform.rotation.eulerAngles;
        rotationVector.y = rotationVector.y + vitesseRotation;
        gameObject.transform.rotation = Quaternion.Euler(rotationVector);
    }
}
