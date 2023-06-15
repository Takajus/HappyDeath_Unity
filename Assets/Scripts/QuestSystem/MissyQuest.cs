using System;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class QuestData
{
    public ResidentData npc;
    public List<Quest> availableQuests = new List<Quest>();
    public List<DialogueData> dialogList = new List<DialogueData>();
}
public class MissyQuest : MonoBehaviour, IInteractable
{
    public QuestManager questManager;

    public List<QuestData> questDataList = new List<QuestData>();
    private int currentQuestIndex = 0;
    [SerializeField] private GameObject E_Input;
    Dialogue _dialogue;

    public static bool isDialogOpen = false;

    private void Start()
    {
        questManager = QuestManager.Instance;
        _dialogue = transform.GetComponent<Dialogue>();
        //_dialogue.dialog = ResidentData.dialogueData;
        if (questDataList.Count <= 0)
        {
            Debug.Log("NO Quest available");
        }

    }

    private void End()
    {
        if (_dialogue.dialog.isDisplay == true)
        {
            if (currentQuestIndex > 0)
            {
                if (!questDataList[currentQuestIndex - 1].availableQuests[currentQuestIndex - 1].isCompleted)
                {
                    Debug.Log("Last Quest not finished yet");
                    _dialogue.dialog.isDisplay = false;
                    //EndInteract();
                    InteractionManager.Instance.InteruptInteraction();
                        //interactHandler.ClearHandler();
                    return;
                }
            }
            else
            {
                if (!questDataList[currentQuestIndex].availableQuests[currentQuestIndex].isCompleted && questManager.activeQuests.Count > 0)
                {
                    Debug.Log("Last Quest not finished yet");
                    _dialogue.dialog.isDisplay = false;
                    //EndInteract();
                    InteractionManager.Instance.InteruptInteraction();
                        //interactHandler.ClearHandler();
                    return;
                }
                Debug.Log("GiveQuest call");
                GiveQuest();
                //EndInteract();
                InteractionManager.Instance.InteruptInteraction();
                    //interactHandler.ClearHandler();
            }
            
        }
        
    }

    public void EndInteract()
    {
        isDialogOpen = false;
        PlayerController.Instance.EnablePlayer();
        _dialogue.EndDiag -= End;
    }
    
    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }

    // Call this method when interacting with MissyQuest to give a quest
    public void GiveQuest()
    {
       
        if (currentQuestIndex < questDataList.Count && questManager.currentQuest == null)
        {
            Quest quest = questDataList[currentQuestIndex].availableQuests[currentQuestIndex];
            
            //_dialogue.dialog = questDataList[currentQuestIndex].dialogList[currentQuestIndex];
            // Remove the quest from the available quests list
            //questDataList.RemoveAt(currentQuestIndex);

            // Send the quest to the QuestManager to accept
            questManager.AcceptQuest(quest);
            Debug.Log("Got Quest " + currentQuestIndex);
        }
    }

    public void Hover()
    {
        E_Input.SetActive(true);
    }

    public void Interact()
    {
        isDialogOpen = true;

        _dialogue.EndDiag += End;
        PlayerController.Instance.DisablePlayer();
        if (currentQuestIndex < questDataList.Count && questManager.currentQuest == null && currentQuestIndex > 0)
        {
            currentQuestIndex++;
        }
        
        if (_dialogue.dialog != questDataList[currentQuestIndex].dialogList[currentQuestIndex] || _dialogue.dialog is null)
        {
            _dialogue.dialog = questDataList[currentQuestIndex].dialogList[currentQuestIndex];
        }
        
        _dialogue.NextDialog();
        
    }


    public void UnHover()
    {
        E_Input.SetActive(false);
    }
    
}
