using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialog;
    public DialogueData genericDialog;

    public Action<DialogueData> EndDiag;

    public void NextDialog()
    {
        if (dialog is null)
        {
            Debug.Log("Generic Dialog Displayed !");
            genericDialog.dialogState = DialogType.StartDialog;
            if (genericDialog.index == 0 && !genericDialog.isDisplay)
            {
                InputManager.Instance.uiDialogAction.action.performed += Next;
                InputManager.Instance.uiCancelAction.action.performed += InteruptedDialogue;
                DialogUI.instance.SetActive(true);
            }
            
            if ((int)dialog.dialogState >= dialog.dialogs.Count)
            {
                EndDialog();
                return;
            }
            if (genericDialog.index < genericDialog.dialogs[(int)genericDialog.dialogState].dialogParts.Count)
            {
                DisplayDialog(genericDialog);
                Debug.Log("dialogue ++");
                ++genericDialog.index;
            }
            else
            {
                EndDialog();
            }
            return;
        }
        
        if (dialog.index == 0 && !dialog.isDisplay)
        {
            InputManager.Instance.uiDialogAction.action.performed += Next;
            InputManager.Instance.uiCancelAction.action.performed += InteruptedDialogue;
            DialogUI.instance.SetActive(true);
        }

        switch (dialog.dialogState)
        {
            case DialogType.None:
                if (dialog.index > 0)
                    dialog.index = 0;

                dialog.dialogState = DialogType.StartDialog;
                dialog.isDisplay = true;
                //InputManager.Instance.uiDialogAction.action.performed += context => Next();
                NextDialog();
                return;
            case DialogType.StartDialog:
                if ((int)dialog.dialogState >= dialog.dialogs.Count)
                {
                    EndDialog();
                    return;
                }
                if (dialog.index < dialog.dialogs[(int)dialog.dialogState].dialogParts.Count)
                {
                    DisplayDialog(dialog);
                    break;
                }
                else
                {
                    dialog.dialogState = DialogType.LoopDialog;
                    EndDialog();
                    return;
                }
            case DialogType.LoopDialog:
                if ((int)dialog.dialogState >= dialog.dialogs.Count)
                {
                    EndDialog();
                    return;
                }
                if (dialog.index < dialog.dialogs[(int)dialog.dialogState].dialogParts.Count)
                {
                    DisplayDialog(dialog);
                    break;
                }
                else
                {
                    EndDialog();
                    return;
                }
            case DialogType.EndDialog:
                if ((int)dialog.dialogState >= dialog.dialogs.Count)
                {
                    EndDialog();
                    return;
                }
                if (dialog.index < dialog.dialogs[(int)dialog.dialogState].dialogParts.Count)
                {
                    DisplayDialog(dialog);
                    break;
                }
                else
                {
                    EndDialog();
                    dialog.dialogState = DialogType.None;
                    dialog = null;
                    return;
                }
        }
        Debug.Log("dialogue ++");
        ++dialog.index;
    }

    private void DisplayDialog(DialogueData dialogue)
    {
        dialogue.isDisplay = true;
        string name, paragraphe;
            
        name = dialogue.dialogs[(int)dialogue.dialogState].dialogParts[dialogue.index].characterName;
        paragraphe = dialogue.dialogs[(int)dialogue.dialogState].dialogParts[dialogue.index].paragraphe;
        DialogUI.instance.UpdateUI(name,paragraphe);
    }

    public void EndDialog()
    {
        DialogueData temp = dialog;
        if (dialog is null)
            temp = genericDialog;

        temp.index = 0;
        
        DialogUI.instance.SetActive(false);
        InputManager.Instance.uiDialogAction.action.performed -= Next;
        InputManager.Instance.uiCancelAction.action.performed -= InteruptedDialogue;
        EndDiag?.Invoke(temp);
    }

    /*private void Update()
    {
        if (!dialog.isDisplay /*dialog?.index > 0#1#)
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                //Debug.Log("truc");
                NextDialog();
            }
        }
    }*/

    public void Next(InputAction.CallbackContext context)
    {
        NextDialog();
    }

    public void InteruptedDialogue(InputAction.CallbackContext context)
    {
        EndDialog();
    }
}

