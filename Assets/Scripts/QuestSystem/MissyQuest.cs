using Fungus;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MissyQuest : MonoBehaviour, IInteractable
{
    public QuestManager questManager;
    public List<Quest> availableQuests = new List<Quest>();
    public List<GameObject> dialogList  = new List<GameObject>();
    private int currentQuestIndex = 0;

    public void EndInteract()
    {
        if (currentQuestIndex > 0)
        {
            if (!availableQuests[currentQuestIndex - 1].isCompleted)
            {
                Debug.Log("Last Quest not finished yet");
                return;
            }
        }

        GiveQuest();
        Debug.Log("Got Quest " + currentQuestIndex);
    }
    
    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }

    // Call this method when interacting with MissyQuest to give a quest
    public void GiveQuest()
    {
       
        if (currentQuestIndex < availableQuests.Count && questManager.currentQuest == null)
        {
            Quest quest = availableQuests[currentQuestIndex];

            // Remove the quest from the available quests list
            availableQuests.RemoveAt(currentQuestIndex);

            // Send the quest to the QuestManager to accept
            questManager.AcceptQuest(quest);

            
        }

     
    }

    private void Update()
    {
        if(availableQuests.Count <= 0)
        {
            Debug.Log("NO Quest available");
        }

        //if (Input.GetKeyDown(KeyCode.E)) {
          
        //    if(currentQuestIndex > 0 ) 
        //    {
        //        if (!availableQuests[currentQuestIndex - 1].isCompleted)
        //        {
        //            Debug.Log("Last Quest not finished yet");
        //            return;
        //        }
        //    }
           
        //    GiveQuest();
        //    Debug.Log("Got Quest " + currentQuestIndex);
        //}
    }

    public void Hover()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {

         if (currentQuestIndex > 0)
        {
            if (!availableQuests[currentQuestIndex - 1].isCompleted)
            {
                Debug.Log("Last Quest not finished yet");
                return;
            }
        }
        dialogList[currentQuestIndex].gameObject.SetActive(true);

    }


    public void UnHover()
    {
        throw new System.NotImplementedException();
    }

    private void Start()
    {
        if(availableQuests.Count != dialogList.Count)
        {
            Debug.LogWarning("Le n de dialog ne correspond pas au n de quest");
        }

      if(availableQuests.Count <= 0)
        {
            Debug.Log("NO Quest available");
        }
    }







    //// Call this method to check if MissyQuest has available quests
    //public bool HasAvailableQuests()
    //{
    //    return currentQuestIndex < availableQuests.Count;
    //}
    //public void IncrementQuestIndex()
    //{
    //    currentQuestIndex++;
    //}
    //// Call this method to get the next available quest from MissyQuest
    //public Quest GetNextQuest()
    //{
    //    if (availableQuests.Count > 0)
    //    {
    //        return availableQuests[0];
    //    }

    //    return null;
    //}

    //private void OnEnable()
    //{
    //    questManager = GameObject.FindAnyObjectByType<QuestManager>();
    //}
}
