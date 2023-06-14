using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialog;

    public Action EndDiag;

    public  void StartDialog()
    {
        dialog.index = 0;
        DialogUI.instance.SetActive(true);
        if (dialog.index < dialog.diagStartQuest.Count)
        {
            DisplayDialog();
        }
    }

    public void NextDialog()
    {
        if (dialog is null)
        {
            Debug.LogError("No Dialog to Display !");
            return;
        }
        
        if (dialog.index == 0)
        {
            InputManager.Instance.uiDialogAction.action.performed += context => Next();
            DialogUI.instance.SetActive(true);
        }
        
        if (dialog.isLooping)
        {
            if (dialog.index > 0)
            {
                EndDialog();
                return;
            }
            
            DisplayDialog();
        }
        else
        {
            if (dialog.index < dialog.diagStartQuest.Count)
            {
                DisplayDialog();
            }
            else
            {
                if (!dialog.isLooping)
                {
                    dialog.isLooping = true;
                }

                EndDialog();
                return;
            }
        }
        Debug.Log("dialogue ++");
        ++dialog.index;
    }

    private void DisplayDialog()
    {
        dialog.isDisplay = true;
        
        if (dialog.isLooping)
        {
            DialogUI.instance.UpdateUI(dialog.loopDiag.characterName,
                dialog.loopDiag.paragraphe);
            return;
        }
        
        DialogUI.instance.UpdateUI(dialog.diagStartQuest[dialog.index].characterName,
            dialog.diagStartQuest[dialog.index].paragraphe);
    }

    public void EndDialog()
    {
        // TODO: ending dialog logic
        dialog.index = 0;
        DialogUI.instance.SetActive(false);
        EndDiag?.Invoke();
        InputManager.Instance.uiDialogAction.action.performed -= context => Next();
    }

    /*private void Update()
    {
        if (dialog?.index > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                //Debug.Log("truc");
                NextDialog();
            }
        }
    }*/

    private void Next()
    {
        if (dialog?.index > 0)
        {
            NextDialog();
        }
    }

    private void OnDestroy()
    {
        dialog.ResetVariables();
    }
}
