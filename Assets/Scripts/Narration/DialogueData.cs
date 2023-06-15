using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;


[System.Serializable]
#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
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
    public List<DialogueStruct> diagStartQuest = new List<DialogueStruct>();
    public DialogueStruct loopDiag;
    public List<DialogueStruct> diagEndQuest = new List<DialogueStruct>();
    public bool isLooping = false;
    [HideInInspector] public bool isDisplay = false;
    public int index;
    [SerializeField] private int startIndex = 0;

    private void OnEnable()
    {
        index = startIndex;
        isLooping = false;
    }

    public void NextIndex()
    {
        if (index < diagStartQuest.Count)
            ++index;
        else
            isDisplay = false;
    }

    public void ResetVariables()
    {
        index = 0;
        isLooping = false;
    }
}
