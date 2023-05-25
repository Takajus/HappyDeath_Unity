using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IInteractable
{
    public void Hover();
    public void UnHover();
    public void Interact();
    public void EndInteract();
    public InteractMode GetInteractMode();
}

public enum InteractMode { Look, Talk, Use };