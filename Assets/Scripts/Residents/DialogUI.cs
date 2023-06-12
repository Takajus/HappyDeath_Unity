using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text DialogNameTextUI;
    [SerializeField] private TMP_Text DialogTextUI;
    [SerializeField] private GameObject UI;

    
    public static DialogUI instance;
    void Awake() { if (instance == null) { instance = this; DontDestroyOnLoad(gameObject); } else Destroy(gameObject); }

    public void UpdateUI(string name, string text)
    {
        DialogNameTextUI.text = name;
        DialogTextUI.text = text;
    }

    public void SetActive(bool isActive)
    {
        UI.SetActive(isActive);
    }
}
