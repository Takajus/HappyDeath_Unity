using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCManager : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S)){
            print("NPC Spawned!");
        }
    }
}
