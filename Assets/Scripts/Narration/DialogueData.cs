using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Unity.VisualScripting;
using UnityEngine.Serialization;

public enum DialogType
{
    StartDialog = 0,
    LoopDialog = 1,
    EndDialog = 2,
    None = 3
}

[System.Serializable]
#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
public class DialoguePart
{
    public string characterName;
    [TextArea] public string paragraphe;
}

[System.Serializable]
public struct DialogueStruct
{
    public DialogType dialogType;
    [Header("Dialog")]
    public List<DialoguePart> dialogParts;
}

[CreateAssetMenu(fileName = "DialogueData", menuName = "Scriptables/Dialogue", order = 5)]
public class DialogueData : ScriptableObject
{
    public DialogType dialogState;
    public string dialogueName;
    public List<DialogueStruct> dialogs = new List<DialogueStruct>();
    [HideInInspector] public bool isDisplay = false;
    public int index;
    [SerializeField] private int startIndex = 0;

    private void OnEnable()
    {
        index = startIndex;
        dialogState = DialogType.None;
        isDisplay = false;
    }

    public void NextIndex()
    {
        if (index < dialogs.Count)
            ++index;
        else
            isDisplay = false;
    }

    public void ResetVariables()
    {
        index = 0;
        dialogState = DialogType.StartDialog;
    }
}
