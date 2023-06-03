using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class NPCManager : MonoBehaviour
{
    //public NPCQuest npcData;
    public LightingManager.DayCycleState dayCycleState;

    private QuestManager questManager;
    private bool Isday;



    private void Start()
    {
        questManager = FindObjectOfType<QuestManager>();
        DayCycleEvents.OnDayStart += CalleChack;
        DayCycleEvents.OnNightStart += CheckNight;
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)){
            print("NPC Spawned!");  
        }

      
    }
    
    private void CalleChack()
    {
        if(Isday == true)
        {
            Isday = false;
        }

        NPCManager[] npcs = FindObjectsOfType<NPCManager>();

        foreach (NPCManager otherNPC in npcs)
        {
            if (otherNPC != this)
            {
                //questManager.CheckNPCProximity(npcData, otherNPC.npcData);
            }
        }


    }

    private void CheckNight()
    {
      

        if (Isday == false)
        {
            Isday = true;
        }
    }

   
}
