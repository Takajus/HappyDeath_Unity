using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using NaughtyAttributes;

public class NPCManager : MonoBehaviour
{
    [SerializeField] NPCs[] NPC;

    [SerializeField] private bool isDay = true;

    void Start()
    {
        LightingManager.Instance._isDay += IsDay;
        LightingManager.Instance._isNight += IsNight;
    }

    private void IsDay()
    {
        if (isDay == true)
            return;

        isDay = true;
    }

    private void IsNight()
    {
        if (isDay == false)
            return;

        isDay = false;
    }

    private void GetNPCInfos()
    {
        NPCs n = Array.Find(NPCs, NPCs => NPC.Name == name);
        if (n == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        Debug.Log("");
    }
}
