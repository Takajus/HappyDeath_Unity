using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IInteractable
{
    MeshRenderer mr;

    [SerializeField] Material baseMaterial;
    [SerializeField] Material hoverMaterial;
    [SerializeField] Material selectMaterial;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
    }

    public void Interact()
    {
        mr.material = selectMaterial;
        Debug.Log("Enter select");
    }

    public void EndInteract()
    {
        Debug.Log("Exit select");
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
