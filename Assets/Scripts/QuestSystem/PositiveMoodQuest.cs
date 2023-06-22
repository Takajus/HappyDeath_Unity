using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Missy Burial Quest", menuName = "PositiveMoodQuest")]
public class PositiveMoodQuest : QuestData
{
    public ResidentData resident;
    
    public override void CheckQuestUpdate()
    {
        switch (questStatus)
        {
            case QuestStatus.StandBy:
                if (resident.isAssign == true)
                    questStatus = QuestStatus.InProgress;
                break;
            case QuestStatus.InProgress:
                if (resident.isAssign != true)
                    questStatus = QuestStatus.StandBy;
                else
                {
                    if (resident.mood > 0)
                    {
                        questStatus = QuestStatus.Completed;
                    }
                }
                break;
            case QuestStatus.Completed:
                break;
        }
    }
}
