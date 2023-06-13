using Fungus;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


[System.Serializable]
public class QuestData
{
    public GameObject npc;
    public List<Quest> availableQuests = new List<Quest>();
    public List<GameObject> dialogList = new List<GameObject>();
}
public class MissyQuest : MonoBehaviour, IInteractable
{
    public QuestManager questManager;

    public List<QuestData> questDataList = new List<QuestData>();
    private int currentQuestIndex = 0;
    [SerializeField] private GameObject E_Input;
    Dialogue _dialogue;

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

    public void EndInteract()
    {
        /*if (currentQuestIndex > 0)
        {
            if (!questDataList[currentQuestIndex - 1].availableQuests[currentQuestIndex - 1].isCompleted)
            {
                Debug.Log("Last Quest not finished yet");
                return;
            }
        }*/

        Debug.Log(_dialogue.dialog.index);
        if (_dialogue.dialog.index >= _dialogue.dialog.diagStructs.Count - 1)
        {
            Debug.Log("GiveQuest call");
            GiveQuest();
        }
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

            // Remove the quest from the available quests list
            questDataList.RemoveAt(currentQuestIndex);

            // Send the quest to the QuestManager to accept
            questManager.AcceptQuest(quest);
            Debug.Log("Got Quest " + currentQuestIndex);

            currentQuestIndex++;
        }
        currentQuestIndex++;
     
    }

    public void Hover()
    {
        E_Input.SetActive(true);
    }

    public void Interact()
    {
        /*if (currentQuestIndex > 0)
        {
            if (!questDataList[currentQuestIndex - 1].availableQuests[currentQuestIndex - 1].isCompleted)
            {
                Debug.Log("Last Quest not finished yet");
                return;
            }
        }*/

        //questDataList[currentQuestIndex].dialogList[currentQuestIndex].SetActive(true);
        /*if (!temp)
        {
            temp = true;
            QuestSystem.Instance.GetDemoTask(1);
        }*/

        //Debug.Log("ca marche?");


        _dialogue.NextDialog();
    }


    public void UnHover()
    {
        E_Input.SetActive(false);
    }

   







}
