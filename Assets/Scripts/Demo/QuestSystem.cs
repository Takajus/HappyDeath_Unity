using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestSystem : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text ToDoTextUI;


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
}
