using System;
using System.Collections.Generic;
using System.Linq;
using Fungus;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class MoodManager : MonoBehaviour
{
    // TODO: Supprimer ici
    [SerializeField]
    private static List<ResidentData> residentList = new List<ResidentData>();
    public Image posBar;
    public Image negBar;

    public float moodAverage;
    
    private static MoodManager instance;

    public static MoodManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<MoodManager>();

            return instance;
        } 
    }

    public void CalculateAverageMood()
    {
        residentList = InventoryManager.Instance.inventoryDatabase.allResidents.FindAll(resid => resid.isAssign == true); 

        if (residentList.Count < 1)
        {
            Debug.LogError("No Residents here !!!!");
            return;
        }

        float totalMood = 0f;
        foreach (var resident in residentList)
        {
            totalMood += resident.mood;
        }
        moodAverage = totalMood / residentList.Count;
        Debug.Log(moodAverage);

        if (moodAverage > 0)
            posBar.fillAmount = moodAverage;
        else
            negBar.fillAmount = -moodAverage;
    }
}
