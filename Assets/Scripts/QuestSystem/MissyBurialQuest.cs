using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missy Burial Quest", menuName = "MissyBurialQuest")]
public class MissyBurialQuest : QuestData
{
    public ResidentData NewDeadNPC;
    
    public override void CheckQuestUpdate()
    {
        switch (questStatus)
        {
            case QuestStatus.StandBy:
                if (NewDeadNPC.isAssign == true)
                {
                    questStatus = QuestStatus.Completed;
                    break;
                }
                InventoryManager.Instance.AddResident(NewDeadNPC);
                questStatus = QuestStatus.InProgress;
                break;
            case QuestStatus.InProgress:
                if(NewDeadNPC.isAssign == true)
                    questStatus = QuestStatus.Completed;
                break;
            case QuestStatus.Completed:
                break;
        }
    }
}
