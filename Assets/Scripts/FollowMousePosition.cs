using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FollowMousePosition : MonoBehaviour
{
    [SerializeField] InputActionReference _mousePosition;
    [SerializeField] Camera _referenceCamera;
    [SerializeField] Canvas _canvas;
    [SerializeField] RectTransform _rect;

    private void Start()
    {
        if (_referenceCamera == null)
            _referenceCamera = Camera.main;

        if (_rect == null)
            _rect = GetComponent<RectTransform>();

        if (_canvas == null)
            _canvas = FindObjectOfType<Canvas>();
    }

    void Update()
    {
        _rect.anchoredPosition = (_mousePosition.action.ReadValue<Vector2>() - _canvas.pixelRect.size/2) / _canvas.scaleFactor;
    }
}
