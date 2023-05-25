using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager : MonoBehaviour
{
    public QuestSystem questUI;
    public List<Quest> activeQuests = new List<Quest>();
    private int currentQuestIndex = 0;


    public void UpdateQuestUI()
    {
        if (currentQuestIndex < activeQuests.Count)
        {
            Quest currentQuest = activeQuests[currentQuestIndex];
            questUI.InitializeQuestUI(currentQuest);
        }
        else
        {
            // No more quests remaining
            questUI.ClearQuestUI();
        }
    }





    public void AddQuest(Quest quest)
    {
        activeQuests.Add(quest);
    }
   


    //public void CheckNPCProximity(NPCQuest npc1, NPCQuest npc2)
    //{
    //    float proximityThreshold = 2f; // Define the distance threshold for proximity

    //    float distance = Vector3.Distance(npc1.position, npc2.position);

    //    if (distance <= proximityThreshold)
    //    {
    //        // NPCs are close to each other, update quest progress or mark as completed
    //        if (currentQuestIndex < activeQuests.Count)
    //        {
    //            Quest currentQuest = activeQuests[currentQuestIndex];
    //            currentQuest.isCompleted = true;

    //            // Update the quest UI with the completed quest information
    //            UpdateQuestUI();

    //            // Check if player wants to accept the next quest
    //            AskPlayerForNextQuest();
    //        }
    //    }
    //}

    private void AskPlayerForQuestResponse(Quest quest)
    {
        // Display a dialogue or UI prompting the player to accept or refuse the quest
        // You can use buttons or any other interaction method to handle the player's response
        // For simplicity, let's assume the player accepts the quest

        AcceptNextQuest();
    }
   
  

    public void AcceptNextQuest( )
    {
        //activeQuests[currentQuestIndex]
        // Handle accepting the quest
        // You can update the quest status, add it to the player's quest log, etc.
        if (currentQuestIndex >= activeQuests.Count)
        {
            currentQuestIndex = 0;
        }
        else
        {
            // Move to the next quest
            currentQuestIndex++;
        }
        // Move to the next quest
        UpdateQuestUI();
    }

    public void RefuseQuest( )
    {

        if (currentQuestIndex < quests.Count)
        {
            Quest quest = quests[currentQuestIndex];
            // Handle refusing the quest
            // You can update the quest status, remove it from the player's quest log, etc.

            // Move to the next quest
          
        }

        // Handle refusing the quest
        // You can update the quest status, remove it from the player's quest log, etc.
        if (currentQuestIndex >= activeQuests.Count)
        {
            currentQuestIndex = 0;
        }
        else
        {
            // Move to the next quest
            currentQuestIndex++;
        }
        
        UpdateQuestUI();
    }




}

