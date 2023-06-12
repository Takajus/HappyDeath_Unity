using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class QuestManager :  MonoBehaviour
{
    public Quest currentQuest; // The current active quest
    public QuestSystem questUI;
    public List<Quest> activeQuests = new List<Quest>();
    private int currentQuestIndex = 0;

    private static QuestManager instance;

    // Method to accept a quest
    public void AcceptQuest(Quest quest)
    {
        // Vérifier si le joueur n'a pas déjà une quête en cours
        if (currentQuest != null)
        {
            Debug.Log("Vous avez déjà une quête en cours.");
            return;
        }

        // Assigner la nouvelle quête en cours
        currentQuest = quest;
        currentQuestIndex++;

        // Ajouter la quête à la liste des quêtes actives
        activeQuests.Add(currentQuest);

        // Mettre à jour l'interface utilisateur
        UpdateQuestUI();
    }

    public void CheckQuestProgress(Quest quest)
    {
        // Vérifier les objectifs de quête et marquer la quête comme complétée si nécessaire
        if (quest.isCompleted)
        {
            CompleteQuest(quest);
        }
    }

    public void ReceiveNewQuest(Quest quest)
    {
        currentQuest = quest;
        UpdateQuestUI();
    }

    public void CompleteQuest(Quest quest)
    {
        // Marquer la quête comme complétée
        quest.isCompleted = true;

        // Supprimer la quête terminée de la liste des quêtes actives
        activeQuests.Remove(quest);

        currentQuest = null;
        UpdateQuestUI();
    } 

    // Method to update the quest UI
    private void UpdateQuestUI()
    {
        questUI.UpdateQuestUI(currentQuest);
    }
    public static QuestManager Instance
    {
        get
        {
            if (instance == null)
                instance = FindObjectOfType<QuestManager>();

            return instance;
        }
    }

}

