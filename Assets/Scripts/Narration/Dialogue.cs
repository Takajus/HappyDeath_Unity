using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueData dialog;

    public  void StartDialog()
    {
        dialog.index = 0;
        DialogUI.instance.SetActive(true);
        if (dialog.index < dialog.diagStructs.Count)
        {
            DisplayDialog();
        }
    }

    public void NextDialog()
    {
        if (dialog.index == 0)
        {
            DialogUI.instance.SetActive(true);
        }
        
        if (dialog.isLooping)
        {
            if (dialog.index > 0)
            {
                dialog.index = 0;
                EndDialog();
                return;
            }
            
            DisplayDialog();
        }
        else
        {
            if (dialog.index < dialog.diagStructs.Count)
            {
                DisplayDialog();
            }
            else
            {
                if (!dialog.isLooping)
                {
                    dialog.index = 0;
                    dialog.isLooping = true;
                }

                dialog.index = 0;
                EndDialog();
                return;
            }
        }
        
        ++dialog.index;
    }

    private void DisplayDialog()
    {
        if (dialog.isLooping)
        {
            DialogUI.instance.UpdateUI(dialog.loopDiag.characterName,
                dialog.loopDiag.paragraphe);
            return;
        }
        
        DialogUI.instance.UpdateUI(dialog.diagStructs[dialog.index].characterName,
            dialog.diagStructs[dialog.index].paragraphe);
    }

    public void EndDialog()
    {
        // TODO: ending dialog logic
        DialogUI.instance.SetActive(false);
    }

    private void Update()
    {
        if (dialog?.index > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                NextDialog();
            }
        }
    }
}
