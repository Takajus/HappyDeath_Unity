using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using TMPro;

public class Dialog : MonoBehaviour, IInteractable
{
    [Header("UI")]
    [SerializeField] private TMP_Text DialogNameTextUI;
    [SerializeField] private TMP_Text DialogTextUI;


    [Header("Demo Text (WIP)")]
    [Multiline(1), SerializeField] private string[] nameText;
    [Multiline(3), SerializeField] private string[] dialogText;


    [Header("Dev variable")]
    [SerializeField] private GameObject DialogPanel;
    [SerializeField] private int nextQuestIndex;
    private int DemoTaskStat = 0;

    [SerializeField] GameObject E_Input;

    /*public static Dialog instance { get; private set; }
    public static Dialog Instance { get { if (instance == null) instance = FindObjectOfType<Dialog>(); return instance; } }*/


    void Start()
    {
        if (!DialogPanel)
            return;

        if (DialogPanel.activeInHierarchy)
            DialogPanel.SetActive(false);

#if UNITY_EDITOR
        if (dialogText.Length != nameText.Length)
        {
            Debug.LogWarning("Dialog and name not same Length !");
            EditorApplication.isPlaying = false;
        }
#endif

        DemoTaskStat = 0;
    }

    private void Update()
    {
        if (DialogPanel.activeInHierarchy)
            if (Input.GetKeyDown(KeyCode.Space))
                NextDemoTask();
    }

    private void TaskDisplayer()
    {
        if (DemoTaskStat != 0 && DemoTaskStat < dialogText.Length)
        {
            DialogTextUI.text = dialogText[DemoTaskStat-1];
            DialogNameTextUI.text = nameText[DemoTaskStat-1];
        }
        else
        {
            DialogNameTextUI.text = "Jane Doe";
            DialogTextUI.text = "Dialogue index error ! Please contact a programmer";
        }

        if (DemoTaskStat != 0 && DemoTaskStat < dialogText.Length)
            DialogPanel.SetActive(true);
        else if (DemoTaskStat == dialogText.Length)
        {
            QuestSystem.Instance.GetDemoTask(nextQuestIndex);
            DialogPanel.SetActive(false);
        }
        else
            DialogPanel.SetActive(false);
    }

    public void NextDemoTask()
    {
        ++DemoTaskStat;
        TaskDisplayer();
    }

    public void Hover()
    {
        E_Input.SetActive(true);
    }

    public void UnHover()
    {
        E_Input.SetActive(false);
    }

    public void Interact()
    {
        E_Input.SetActive(false);
        NextDemoTask();
    }

    public void EndInteract()
    {
    }

    public InteractMode GetInteractMode()
    {
        throw new System.NotImplementedException();
    }
}
