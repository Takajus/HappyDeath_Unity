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
#if UNITY_EDITOR
[UnityEditor.InitializeOnLoad]
#endif
[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class QuestData : ScriptableObject
{
    [Header("Quest Description")]
    public string questTitle;
    [TextArea] public string questDescription;
    [Header("Linked Dialog")]
    public DialogueData questDialog;
    
    public QuestStatus questStatus;
    //public ResidentData NewDeadNPC;
    //public bool isCompleted, inProgress;
    
    public virtual void CheckQuestUpdate()
    {
        // Methode pour les sous-class
    }

    public void OnEnable()
    {
        questStatus = QuestStatus.StandBy;
    }
}
