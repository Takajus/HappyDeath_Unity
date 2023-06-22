using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager :  MonoBehaviour
{
    public QuestData currentQuest; // The current active quest
    public QuestSystem questUI;
    public List<QuestData> activeQuests = new List<QuestData>();
    private int currentQuestIndex = 0;

    private static QuestManager instance;
    
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<QuestManager>();

            return instance;
        }
    }

    // Method to accept a quest
    public void AcceptQuest(QuestData quest)
    {
        // V�rifier si le joueur n'a pas d�j� une qu�te en cours
        if (currentQuest != null)
        {
            Debug.Log("Vous avez d�j� une qu�te en cours.");
            return;
        }

        // Assigner la nouvelle qu�te en cours
        currentQuest = quest;
        currentQuestIndex++;

        // Ajouter la qu�te � la liste des qu�tes actives
        activeQuests.Add(currentQuest);

        /*if (quest is MissyBurialQuest)
        {
            MissyBurialQuest missyQuest = (MissyBurialQuest)quest;
            InventoryManager.Instance.AddResident(missyQuest.NewDeadNPC);
        }*/
        
        quest.CheckQuestUpdate();
        
        if(quest is MissyBurialQuest)
            Tomb.OnAssignNPC += CheckQuestProgress;
        else if (quest is PositiveMoodQuest)
            DayCycleEvents.OnDayStart += CheckQuestProgress;

            // Mettre � jour l'interface utilisateur
        UpdateQuestUI();
    }

    public void CheckQuestProgress(/*ResidentData residentData, Tomb tomb*/)
    {
        currentQuest.CheckQuestUpdate();
        if (currentQuest.questStatus == QuestStatus.Completed)
        {
            CompleteQuest(currentQuest);
        }
    }

    public void ReceiveNewQuest(QuestData quest)
    {
        currentQuest = quest;
        UpdateQuestUI();
    }

    public void CompleteQuest(QuestData quest)
    {
        // Marquer la qu�te comme compl�t�e
        //quest.questStatus = QuestStatus.Completed;
        quest.questDialog.dialogState = DialogType.EndDialog;

        // Supprimer la qu�te termin�e de la liste des qu�tes actives
        //activeQuests.Remove(quest);
        
        if(quest is MissyBurialQuest)
            Tomb.OnAssignNPC -= CheckQuestProgress;

        currentQuest = null;
        UpdateQuestUI();
    } 

    // Method to update the quest UI
    private void UpdateQuestUI()
    {
        questUI.UpdateQuestUI(currentQuest);
    }

    private void OnDestroy()
    {
        Tomb.OnAssignNPC -= CheckQuestProgress;
        DayCycleEvents.OnNightStart -= CheckQuestProgress;
    }

    private void Awake()
    {
    }
}

