using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New NPC", menuName = "NPC")]
public class NPCQuest : ScriptableObject
{
    public NPC[] NpcObject;
    public string npcName;
    public Vector3 position;
}