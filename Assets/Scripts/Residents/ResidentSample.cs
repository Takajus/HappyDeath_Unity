using System.Collections.Generic;
using UnityEngine;

public enum LikeDislike
{
    Like,
    Dislike
}
public enum Category
{
    Object,
    Social,
    Place
}

public enum Race
{
    Wolf,
    Banshee,
    Skeleton,
    General,
    Null
}

public enum ObjectType
{
    Wood,
    Flower,
    Stone,
    Decoration,
    Water,
    Null
}

public enum Role
{
    Chief,
    Spawn
}

[System.Serializable]
public class ElementPreference
{
    public Category preferenceType;
    public Race すき;
    public ObjectType objectLike;
    public int amount;
    public LikeDislike likeDislike;
}

[CreateAssetMenu(fileName = "ResidentSample", menuName = "Scriptables/Resident", order = 4)]
public class ResidentSample : ScriptableObject
{
    public GameObject prefab;
    
    public string residentName;
    [TextArea] public string description;
    //public Sprite sprite;

    public Race race;
    public Role role;
    
    public List<ElementPreference> elementList;
    
    [Header("Secret")]
    public string シークレット;

    [HideInInspector]
    public float mood = 0;
}
