using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance { get { if (instance == null) instance = FindObjectOfType<InputManager>(); return instance; } }
    private static InputManager instance;

    [Header("Game")]
    public InputActionReference gameInteractAction;
    public InputActionReference gameMouseInteractAction;
    public InputActionReference gameCancelAction;
    public InputActionReference gameMousePosition;

    [Header("UI")]
    public InputActionReference uiInteractAction;
    public InputActionReference uiMouseInteractAction;
    public InputActionReference uiCancelAction;
    public InputActionReference uiMousePosition;

    [Header("Editor")]
    public InputActionReference editorInteractAction;
    public InputActionReference editorMouseInteractAction;
    public InputActionReference editorCancelAction;
    public InputActionReference editorMousePosition;
    public InputActionReference editorMultiplicatorValue;
}
