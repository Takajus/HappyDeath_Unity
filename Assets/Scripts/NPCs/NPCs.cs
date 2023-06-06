using UnityEngine;

[System.Serializable]
public class NPCs
{
    private enum Race { WOLF, BANSHEE, SKELETON }
    private enum Element { FLOWER, FRIENDS, ALONE, WATER, STONE, WOLF, BANSHEE, SKELETON }

    [SerializeField] private string name;
    [SerializeField] private GameObject tomb;
    [SerializeField] private GameObject npcAsset;

    [SerializeField] private Race race;
    [SerializeField] private Element like;
    [SerializeField] private Element dislike;

    private bool hasRequest;
}
