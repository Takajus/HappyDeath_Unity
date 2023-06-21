using System.Collections;
using System.Collections.Generic;
using NaughtyAttributes;
using UnityEngine;

public enum QuestStatus
{
    StandBy,
    InProgress,
    Completed
}

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestData : ScriptableObject
{
    [Header("Quest Description")]
    public string questTitle;
    [TextArea] public string questDescription;
    [Header("Linked Dialog")]
    public DialogueData questDialog;
    
    [HideInInspector] public QuestStatus questStatus;
    //public ResidentData NewDeadNPC;
    //public bool isCompleted, inProgress;
    
    public virtual void CheckQuestUpdate()
    {
        // Methode pour les sous-class
    }

    public void Init()
    {
        questStatus = QuestStatus.StandBy;
    }
}
