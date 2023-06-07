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
        // V�rifier les objectifs de qu�te et marquer la qu�te comme compl�t�e si n�cessaire
        if (quest.isCompleted)
        {
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

        currentQuest = null;
        UpdateQuestUI();
    } 

    // Method to update the quest UI
    private void UpdateQuestUI()
    {
        questUI.UpdateQuestUI(currentQuest);
    }
}

