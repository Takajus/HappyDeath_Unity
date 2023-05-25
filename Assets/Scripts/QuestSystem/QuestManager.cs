using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public QuestSystem questUI;
    public Quest activeQuest;
    public List<Quest> activeQuests = new List<Quest>();

    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }
    public void UpdateQuestUI(Quest quest)
    {
        if (activeQuest != null)
        {
            questUI.UpdateQuestUI(activeQuest.questTitle, activeQuest.questDescription);
        }
    }

    public void CheckNPCProximity(NPCQuest npc1, NPCQuest npc2)
    {
        float proximityThreshold = 2f; // Define the distance threshold for proximity

        float distance = Vector3.Distance(npc1.position, npc2.position);

        if (distance <= proximityThreshold)
        {
            // NPCs are close to each other, update quest progress or mark as completed
            activeQuest.isCompleted = true;

            // Update the quest UI with the completed quest information
            UpdateQuestUI(activeQuest);
        }
    }

   


}

