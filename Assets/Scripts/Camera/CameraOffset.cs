using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraOffset : MonoBehaviour
{
    [SerializeField] Transform cameraTransform;
    [SerializeField] Canvas canvas;
    [SerializeField] InputActionReference mousePosition;
    [SerializeField] Vector3 offset;
    [SerializeField, Range(0, 100)] float horizontalMultiplier = 10;
    [SerializeField, Range(0, 100)] float verticalMultiplier = 10;

    void Start()
    {
        if (cameraTransform == null)
            cameraTransform = Camera.main.transform;

        if (canvas == null)
            canvas = FindAnyObjectByType<Canvas>();
    }

    void Update()
    {
        Vector2 pos = (mousePosition.action.ReadValue<Vector2>() - canvas.pixelRect.size / 2) / canvas.scaleFactor;

        pos.x = pos.x / (canvas.pixelRect.size.x / 2) * horizontalMultiplier;
        pos.y = pos.y / (canvas.pixelRect.size.y / 2) * verticalMultiplier;

        transform.localPosition = cameraTransform.TransformDirection(pos) + offset;
    }
}
