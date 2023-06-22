using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ChannelingHUD : MonoBehaviour
{
    private static ChannelingHUD instance;
    public static ChannelingHUD Instance { get { return instance; } }

    [SerializeField] Image fillImage;
    [SerializeField] TextMeshProUGUI percentText;
    [SerializeField] TextMeshProUGUI actionText;

    public void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void StartChanneling(string actionName)
    {
        gameObject.SetActive(true);
        fillImage.fillAmount = 0;
        actionText.text = actionName;
        percentText.text = "0%";
    }

    public void UpdateChanneling(float percentage)
    {
        percentText.text = (percentage * 100).ToString("F0") + "%";
        fillImage.fillAmount = percentage;
    }

    public void EndChanneling()
    {
        gameObject.SetActive(false);
    }
}
