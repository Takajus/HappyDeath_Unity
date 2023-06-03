using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IInteractable, IDiggable
{
    MeshRenderer mr;
    Collider coll;

    [Header("Tile State")]
    public bool isOccupied;
    public bool isDug;
    public bool IsDug { get => isDug; set => isDug = value; }


    [Header("Tile Settings")]
    public float x;
    public float z;
    public List<Tile> neighbors { get; private set; }

    [Header("Other")]
    [SerializeField] Material baseMaterial;
    [SerializeField] Material hoverMaterial;
    [SerializeField] Material selectMaterial;
    [SerializeField] GameObject digDecal;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();

        neighbors = GridManager.Instance.GetNeighbors(this);

        if (IsDug)
            digDecal.SetActive(true);
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


    public void Dig()
    {
        digDecal.SetActive(true);
        IsDug = true;
    }

    public void Fill()
    {
        digDecal.SetActive(false);
        IsDug = false;
    }

    public void DigHover()
    {
        Hover();
    }

    public void DigUnHover()
    {
        UnHover();
    }
}
