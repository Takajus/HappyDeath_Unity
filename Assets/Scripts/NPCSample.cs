using UnityEngine;

[CreateAssetMenu(fileName = "NPCSample", menuName = "Scriptables/NPC", order = 4)]
public class NPCSample : ScriptableObject
{
    public enum TypeCategories
    {
        Object,
        Social,
        Place
    }
    
    public enum Role
    {
        Chief,
        Spawn
    }
    
    public enum NPCType
    {
        Wolf,
        Banshee,
        Skeleton
    }
    
    public enum ObjectType
    {
        
    }

    public GameObject prefab;
    public Sprite sprite;
    public string name;
    public string description;
    public NPCType race;
    public Role role;
    public static float humorStartValue; 
    
    [Header("Like")]
    public TypeCategories like;
    public NPCType すきい;
    public ObjectType objectLike;
    public int amountLike;
    
    [Header("Dislike")]
    public TypeCategories dislike;
    public NPCType きらい;
    public ObjectType objectDislike;
    public int amountDislike;

    [Header("Secret")]
    public string シークレット;


}
