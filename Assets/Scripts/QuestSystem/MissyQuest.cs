using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissyQuest : MonoBehaviour
{
    public QuestManager questManager;
    public List<Quest> availableQuests = new List<Quest>();
    private int currentQuestIndex = 0;


    // Call this method when interacting with MissyQuest to give a quest
    public void GiveQuest()
    {

        if (currentQuestIndex < availableQuests.Count)
        {
            // Get the current quest
            Quest quest = availableQuests[currentQuestIndex];

            // Send the quest to the QuestManager to store
            questManager.AddQuest(quest);

            // Remove the quest from the available quests list
            availableQuests.RemoveAt(currentQuestIndex);

          
        }


    }
    // Call this method to check if MissyQuest has available quests
    public bool HasAvailableQuests()
    {
        return currentQuestIndex < availableQuests.Count;
    }
    public void IncrementQuestIndex()
    {
        currentQuestIndex++;
    }
    // Call this method to get the next available quest from MissyQuest
    public Quest GetNextQuest()
    {
        if (availableQuests.Count > 0)
        {
            return availableQuests[0];
        }

        return null;
    }

    private void OnEnable()
    {
        questManager = GameObject.FindAnyObjectByType<QuestManager>();
    }
}
