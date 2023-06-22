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
    public List<Tile> neighbors = new List<Tile>();

    [Header("Other")]
    [SerializeField] Material baseMaterial;
    [SerializeField] Material hoverMaterial;
    [SerializeField] Material selectMaterial;
    [SerializeField] GameObject digDecal;
    [SerializeField] GameObject digDecalUp;
    [SerializeField] GameObject digDecalDown;
    [SerializeField] GameObject digDecalLeft;
    [SerializeField] GameObject digDecalRight;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        coll = GetComponent<Collider>();

        //neighbors = GridManager.Instance.GetNeighbors(this);

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
        UpdateDigDecal(true);
    }

    public void Fill()
    {
        digDecal.SetActive(false);
        IsDug = false;
        UpdateDigDecal(true);
    }

    public void DigHover()
    {
        Hover();
    }

    public void DigUnHover()
    {
        UnHover();
    }

    public void UpdateDigDecal(bool propagate, Tile target = null)
    {
        if (!isDug)
        {
            digDecal.SetActive(false);
            digDecalLeft.SetActive(false);
            digDecalRight.SetActive(false);
            digDecalUp.SetActive(false);
            digDecalDown.SetActive(false);

            foreach (var neighbor in neighbors)
            {
                if (propagate)
                    neighbor.UpdateDigDecal(false);
            }

            return;
        }

        digDecal.SetActive(true);
        foreach (var neighbor in neighbors)
        {
            if (propagate)
                neighbor.UpdateDigDecal(false);

            if (neighbor.x == x - 1)
            {
                if (neighbor.isDug)
                    digDecalLeft.SetActive(true);
                else
                    digDecalLeft.SetActive(false);
            }
            else if (neighbor.x == x + 1)
            {
                if (neighbor.isDug)
                    digDecalRight.SetActive(true);
                else
                    digDecalRight.SetActive(false);
            }
            else if (neighbor.z == z - 1)
            {
                if (neighbor.isDug)
                    digDecalDown.SetActive(true);
                else
                    digDecalDown.SetActive(false);
            }
            else if (neighbor.z == z + 1)
            {
                if (neighbor.isDug)
                    digDecalUp.SetActive(true);
                else
                    digDecalUp.SetActive(false);
            }
        }
    }
}
