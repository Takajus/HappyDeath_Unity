using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoodManager : MonoBehaviour
{
    // TODO: Supprimer ici
    public static List<Resident> residentList = new List<Resident>();
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
        if (residentList.Count < 1)
        {
            Debug.LogError("No Residents here !!!!");
            return;
        }

        float totalMood = 0f;
        foreach (var resident in residentList)
        {
            totalMood += resident.ResidentData.mood;
        }
        moodAverage = totalMood / residentList.Count;
        Debug.Log(moodAverage);

        if (moodAverage > 0)
            posBar.fillAmount = moodAverage;
        else
            negBar.fillAmount = -moodAverage;
    }
}
