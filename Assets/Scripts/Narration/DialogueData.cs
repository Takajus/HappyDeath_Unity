using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;

[System.Serializable]
public class DialogueStruct
{
    public string characterName;
    [TextArea]
    public string paragraphe;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptables/Dialogue", order = 5)]
public class DialogueData : ScriptableObject
{
    public string dialogueName;
    public List<DialogueStruct> diagStructs = new List<DialogueStruct>();
    public DialogueStruct loopDiag;
    public bool isLooping = false;
    [HideInInspector] public bool isDisplay;
    public int index;
    [SerializeField] private int startIndex = 0;

    private void Awake()
    {
        index = startIndex;
        isLooping = false;
    }

    public void NextIndex()
    {
        if (index < diagStructs.Count)
            ++index;
        else
            isDisplay = false;
    }
}
