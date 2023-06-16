using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager :  MonoBehaviour
{
    public Quest currentQuest; // The current active quest
    public QuestSystem questUI;
    public List<Quest> activeQuests = new List<Quest>();
    private int currentQuestIndex = 0;

    private static QuestManager instance;

    // Method to accept a quest
    public void AcceptQuest(Quest quest)
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

        InventoryManager.Instance.AddResident(quest.NewDeadNPC);

        // Mettre � jour l'interface utilisateur
        UpdateQuestUI();
    }

    public void CheckQuestProgress(ResidentData residentData, Tomb tomb)
    {
        // V�rifier les objectifs de qu�te et marquer la qu�te comme compl�t�e si n�cessaire
        if(residentData.isAssign == true)
        {
            Quest quest = activeQuests.Find(e => e.NewDeadNPC == residentData);
            if(quest is null)
                return;
            
            CompleteQuest(quest);
        }
    }

    public void ReceiveNewQuest(Quest quest)
    {
        currentQuest = quest;
        UpdateQuestUI();
    }

    public void CompleteQuest(Quest quest)
    {
        // Marquer la qu�te comme compl�t�e
        quest.isCompleted = true;

        // Supprimer la qu�te termin�e de la liste des qu�tes actives
        //activeQuests.Remove(quest);

        currentQuest = null;
        UpdateQuestUI();
    } 

    // Method to update the quest UI
    private void UpdateQuestUI()
    {
        questUI.UpdateQuestUI(currentQuest);
    }
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<QuestManager>();

            return instance;
        }
    }

    private void OnDestroy()
    {
        Tomb.OnAssignNPC -= CheckQuestProgress;
    }

    private void Awake()
    {
        Tomb.OnAssignNPC += CheckQuestProgress;
    }
}

