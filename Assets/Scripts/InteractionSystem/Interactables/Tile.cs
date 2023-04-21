using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IInteractable
{
    MeshRenderer mr;
    Collider coll;

    [Header("Tile State")]
    public bool isOccupied;
    public bool isDug;

    [Header("Tile Settings")]
    public float x;
    public float z;
    public List<Tile> neighbors { get; private set; }

    [Header("Other")]
    [SerializeField] Material baseMaterial;
    [SerializeField] Material hoverMaterial;
    [SerializeField] Material selectMaterial;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();

        neighbors = GridManager.Instance.GetNeighbors(this);
    }

    public void Interact()
    {
        mr.material = selectMaterial;
    }

    public void EndInteract()
    {

    }

    public void Hover()
    {
        mr.material = hoverMaterial;
    }

    public void UnHover()
    {
        mr.material = baseMaterial;
    }

    public InteractMode GetInteractMode()
    {
        return InteractMode.Use;
    }
}
