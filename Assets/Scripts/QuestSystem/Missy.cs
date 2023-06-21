using System;
using Fungus;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


/*[System.Serializable]
public class QuestData
{
    public ResidentData npc;
    public List<Quest> availableQuests = new List<Quest>();
    public List<DialogueData> dialogList = new List<DialogueData>();
}*/

public class Missy : MonoBehaviour, IInteractable
{
    public QuestManager questManager;

    public List<QuestData> questDataList = new();
    
    [HideInInspector] public int currentQuestIndex = 0;
    private Dialogue _dialogue;
    
    [SerializeField] private GameObject E_Input;

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
        else
        {
            foreach (QuestData questData in questDataList)
            {
                questData.Init();
            }
        }
    }

    private void End(DialogueData dialog)
    {
        /*if (_dialogue.dialog.isDisplay == true)
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
                Debug.Log("GiveQuest call");
                GiveQuest();
                //EndInteract();
                InteractionManager.Instance.InteruptInteraction();
                //interactHandler.ClearHandler();
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
        }*/

        // Ce code permet de mettre fin à l'interaction avec Missy si le dialog est désactivé
        if (dialog.isDisplay == true)
        {
            dialog.isDisplay = false;
        }
        InteractionManager.Instance.InteruptInteraction();
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
       
        /*if (currentQuestIndex < questDataList.Count && questManager.currentQuest == null)
        {
            QuestData quest = questDataList[currentQuestIndex].availableQuests[0];
            
            //_dialogue.dialog = questDataList[currentQuestIndex].dialogList[currentQuestIndex];
            // Remove the quest from the available quests list
            //questDataList.RemoveAt(currentQuestIndex);

            // Send the quest to the QuestManager to accept
            questManager.AcceptQuest(quest);
            Debug.Log("Got Quest " + currentQuestIndex);
        }*/
        
        if (currentQuestIndex < questDataList.Count)
        {
            switch (questDataList[currentQuestIndex].questStatus)
            {
                case QuestStatus.StandBy:
                    questManager.AcceptQuest(questDataList[currentQuestIndex]);
                    _dialogue.dialog = questDataList[currentQuestIndex].questDialog;
                    break;
                case QuestStatus.InProgress:
                    break;
                case QuestStatus.Completed:
                    if (questDataList[currentQuestIndex].questDialog.dialogState != DialogType.None)
                    {
                        questDataList[currentQuestIndex].questDialog.dialogState = DialogType.EndDialog;
                        break;
                    }
                    ++currentQuestIndex;
                    break;
            }
        }
        else
            _dialogue.dialog = null;
    }

    public void Hover()
    {
        E_Input.SetActive(true);
    }

    public void Interact()
    {
        //--- DON'T TOUCH HERE ---//
        isDialogOpen = true;
        _dialogue.EndDiag += End;
        PlayerController.Instance.DisablePlayer();
        //------------------------//

        // TODO: Dialog attribution check !
        GiveQuest();
        
        /*if (currentQuestIndex != questDataList.Count)
        {
            if (currentQuestIndex < questDataList.Count && questManager.currentQuest == null &&
                questDataList[currentQuestIndex].availableQuests[currentQuestIndex].isCompleted)
            {
                currentQuestIndex++;
            }

            if (_dialogue.dialog != questDataList[currentQuestIndex].dialogList[0])
            {
                _dialogue.dialog = questDataList[currentQuestIndex].dialogList[0];
            }
        }*/

        _dialogue.NextDialog();
        
    }

    public void UnHover()
    {
        E_Input.SetActive(false);
    }
}
