using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text ToDoTextUI;

    //QuestUI
    [SerializeField] private Text questTitleText;
    [SerializeField] private Text questDescriptionText;
    [SerializeField] private Button acceptButton;
    [SerializeField] private Button refuseButton;
    private QuestManager questManager;

    [Header("Demo Text (WIP)")]
    [Multiline(3), SerializeField] private string[] taskText;


    [Header("Dev variable")]
    [SerializeField] private GameObject QuestPanel;
    [SerializeField] public bool isTutorial;
    public int DemoTaskStat = 0;
    [SerializeField] private GameObject wood = null, stone = null, flower = null;

    public static QuestSystem instance;
    public static QuestSystem Instance { get { if (instance == null) instance = FindObjectOfType<QuestSystem>(); return instance; } }


    void Start()
    {
        if (!QuestPanel)
            return;

        if(QuestPanel.activeInHierarchy)
            QuestPanel.SetActive(false);

        isTutorial = false;

        DemoTaskStat = 0;

        // Get the QuestManager component attached to the same GameObject
        questManager = GetComponent<QuestManager>();
    }

    private void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.G))
            NextDemoTask();*/

        if (QuestPanel.activeInHierarchy)
            isTutorial = true;
        else
            isTutorial = false;

        if(DemoTaskStat == 1)
            GetPickUp();
    }

    private void TaskDisplayer()
    {
        if(DemoTaskStat != 0 && DemoTaskStat < taskText.Length)
        {
            ToDoTextUI.text = taskText[DemoTaskStat];
        }
        else
            ToDoTextUI.text = "Demo Quest index error ! Please contact a programmer";

        if (DemoTaskStat != 0)
            QuestPanel.SetActive(true);
        else
            QuestPanel.SetActive(false);

        if(DemoTaskStat == 4)
        {
            LightingManager.Instance.SetTime(1800 / 4 * 3.5f);
        }
    }

    public void NextDemoTask()
    {
        ++DemoTaskStat;
        TaskDisplayer();
    }

    public void GetDemoTask(int taskIndex)
    {
        DemoTaskStat = taskIndex;
        TaskDisplayer();
    }

    private void GetPickUp()
    {
        if (wood == null)
            if (stone == null)
                if (flower == null)
                    GetDemoTask(2);
    }

    public void UpdateQuestUI(string title, string description)
    {
        questTitleText.text = title;
        questDescriptionText.text = description;
    }

    //QuestUI for Accepte or refuse the Quest 
    public void InitializeQuestUI(Quest quest)
    {

        questTitleText.text = quest.questTitle;
        questDescriptionText.text = quest.questDescription;

        acceptButton.onClick.AddListener(AcceptQuest);
        refuseButton.onClick.AddListener(RefuseQuest);
    }
    public void ClearQuestUI()
    {
        // Clear the quest UI elements
        questTitleText.text = string.Empty;
        questDescriptionText.text = string.Empty;

        acceptButton.onClick.RemoveAllListeners();
        refuseButton.onClick.RemoveAllListeners();
    }
    private void AcceptQuest()
    {
        // Call the AcceptQuest method in your QuestManager or wherever your quest logic is
      // QuestManager.AcceptNextQuest();

     // Perform any UI updates or transitions after accepting the quest
    }

    private void RefuseQuest()
    {
        // Call the RefuseQuest method in your QuestManager or wherever your quest logic is
     //  QuestManager.RefuseQuest();

     // Perform any UI updates or transitions after refusing the quest
    }
}
