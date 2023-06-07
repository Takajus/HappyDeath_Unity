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
    

    // Method to accept a quest
    public void AcceptQuest(Quest quest)
    {
        if (currentQuest == null)
        {
            currentQuest = quest;
            currentQuestIndex++;
            UpdateQuestUI();
        }
    }

    public void CheckQuestProgress(Quest quest)
    {
        // Vérifier les objectifs de quête et marquer la quête comme complétée si nécessaire
        if (quest.isCompleted)
        {
            CompleteQuest(quest);
        }
    }

    public void CompleteQuest(Quest quest)
    {
        // Marquer la quête comme complétée
        quest.isCompleted = true;

        // Mettre à jour l'interface utilisateur de quête
        UpdateQuestUI();
    } 

    // Method to update the quest UI
    private void UpdateQuestUI()
    {
        if (questUI != null)
        {
            questUI.UpdateQuestUI();
        }
    }
}

