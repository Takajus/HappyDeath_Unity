using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
[CreateAssetMenu(fileName = "Item", menuName = "Scriptables/Item", order = 3)]
public class Item : ScriptableObject
{
    public enum ItemType 
    { 
        TOOL,
        RESOURCE,
        BUILD
    };

    public ItemType itemType;
    public GameObject Prefab;
    public Sprite Sprite;
    public string Name;
    public string Description;
    public int Amount;
    public bool Placeable;
}
