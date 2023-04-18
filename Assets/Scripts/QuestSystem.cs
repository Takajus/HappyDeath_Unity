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
    private int DemoTaskStat = 0;

    public static QuestSystem instance { get; private set; }
    public static QuestSystem Instance { get { if (instance == null) instance = FindObjectOfType<QuestSystem>(); return instance; } }


    void Start()
    {
        if (!QuestPanel)
            return;

        if(QuestPanel.activeInHierarchy)
            QuestPanel.SetActive(false);

        DemoTaskStat = 0;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
            NextDemoTask();
    }

    private void TaskDisplayer()
    {
        if(DemoTaskStat != 0 && DemoTaskStat < taskText.Length)
            ToDoTextUI.text = taskText[DemoTaskStat];
        else
            ToDoTextUI.text = "Demo Quest index error ! Please contact a programmer";

        if (DemoTaskStat != 0)
            QuestPanel.SetActive(true);
        else
            QuestPanel.SetActive(false);

        /*switch (DemoTaskStat)
        {
            case 0:
                QuestPanel.SetActive(false);
                break;
            default:
                ToDoTextUI.text = "Demo Quest index error ! Please contact a programmer";
                break;
        }*/
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
}
