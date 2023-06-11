using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Quest", menuName = "Quest")]
public class Quest : ScriptableObject
{
    public string questTitle;
    public string questDescription;
    public ResidentData NewDeadNPC;
    public bool isCompleted { get; set; }
}
