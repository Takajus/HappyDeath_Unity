using System;
using System.Collections.Generic;
using Fungus;
using Unity.VisualScripting;
using UnityEngine;


public class Resident : MonoBehaviour, IInteractable
{
    public ResidentData ResidentData;
    private ResidentData currentData = null;

    private GameObject modelSlot, model;
    private bool placeIsCheck = false;

    [SerializeField] private float detectionRadius = 5f;
    //[SerializeField] private LayerMask residentLayer, placeLayer, objectLayer;
    private int residentAmount = 0;
    float negativeMood = 0f, positiveMood = 0f;

    private Dialogue _dialogue;

    private Collider col;

    private void Awake()
    {
        // TODO: Modifier la list pour utiliser celle de la DataBase
        col = transform.GetComponent<Collider>();
        col.enabled = false;
        _dialogue = transform.GetComponent<Dialogue>();


        DayCycleEvents.OnNightStart += Day;
        DayCycleEvents.OnDayStart += Night;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;    
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void Night()
    {
        if (ResidentData == null /*|| !ResidentData.isAssign*/)
            return;

        CheckupState(false);

        negativeMood = 0f;
        positiveMood = 0f;
        residentAmount = 0;
        placeIsCheck = false;
        ResidentData.mood = 0;

        if (detectionRadius < 1f)
        {
            return;
        }
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius);
        
        foreach (ElementPreference elementPreference in ResidentData.elementList)
        {
            foreach (Collider col in colliders)
            {
                if (col.gameObject == this.gameObject)
                    continue;
                
                switch (elementPreference.preferenceType)
                {
                    case Category.Social:
                        ResidentData otherResident = col.gameObject.GetComponent<Resident>()?.ResidentData;
                        if (!otherResident) break;

                        if (elementPreference.race == Race.General)
                        {
                            AmountCalcul(elementPreference);
                        }

                        if (elementPreference.race == otherResident.race)
                        {
                            if (elementPreference.likeDislike == LikeDislike.Like)
                            {
                                ++positiveMood;
                                // Debug.Log("I LIKE this Race");
                            }
                            else
                            {
                                ++negativeMood;
                                // Debug.LogWarning("I HATE this Race");
                            }
                        }
                        continue;
                    case Category.Object:
                        
                        Build otherBuild = col.gameObject.GetComponent<Build>();
                        if(!otherBuild) break;

                        RecipeConstitution(elementPreference, otherBuild);
                        continue;
                    case Category.Place:
                        if(placeIsCheck) continue;
                        
                        GameObject otherPlace = col.gameObject.transform.parent?.gameObject.layer == LayerMask.NameToLayer("Zone") ? col.gameObject : null;
                        if(!otherPlace) break;

                        if (elementPreference.objectLike.ToString() == LayerMask.LayerToName(otherPlace.layer))
                        {
                            if (elementPreference.likeDislike == LikeDislike.Like)
                            {
                                ++positiveMood;
                                placeIsCheck = true;
                                // Debug.Log("I LIKE this Place");
                            }
                            else
                            {
                                ++negativeMood;
                                placeIsCheck = true;
                                // Debug.LogWarning("I HATE this Place");
                            }
                        }
                        
                        continue;
                }
            }
        }

        float nbBonus = positiveMood + negativeMood;
        float average = (positiveMood/nbBonus) - (negativeMood/nbBonus);
        if (float.IsNaN(average))
            ResidentData.mood = 0;
        else
            ResidentData.mood = average;
        

        Debug.Log(ResidentData.name + " - Humeur : " + ResidentData.mood);
    }

    private void Day()
    {
        if (ResidentData == null /*|| !ResidentData.isAssign*/)
            return;

        CheckupState(true);
    }

    private void CheckupState(bool isDay)
    {
        if (currentData != ResidentData)
        {
            if(MoodManager.residentList.Contains(currentData))
            {
                MoodManager.residentList.Remove(currentData);
            }
            MoodManager.residentList.Add(ResidentData);
            model = null;
            _dialogue.dialog = null;
            currentData = ResidentData;
        }
        
        if(!_dialogue?.dialog)
            _dialogue.dialog = currentData?.dialogueData;

        if (!isDay)
        {
            col.enabled = true;

            if(!modelSlot)
                modelSlot = transform.Find("Model").gameObject;

            if(!model)
                model = Instantiate(currentData.prefab, modelSlot.transform);
            if (!model.activeInHierarchy)
                model.SetActive(true);
        }
        else
        {
            if (!model)
                return;
            
            if (model.activeInHierarchy)
            {
                model.SetActive(false);
                col.enabled = false;
            }
            
        }
    }

    private void AmountCalcul(ElementPreference elementPreference)
    {
        ++residentAmount;
        
        if (elementPreference.amount < 1)
        {
            if (residentAmount > elementPreference.amount)
                return;
            if (elementPreference.likeDislike == LikeDislike.Like)
            {
                ++positiveMood;
                Debug.Log("I LIKE been Alone");
            }
            else
            {
                ++negativeMood;
                Debug.LogWarning("I HATE been Alone");
            }
        }
        else
        {
            if (residentAmount < elementPreference.amount || residentAmount > elementPreference.amount)
                return;
            if (elementPreference.likeDislike == LikeDislike.Like)
            {
                ++positiveMood;
                Debug.Log("I LIKE People");
            }
            else
            {
                ++negativeMood;
                Debug.LogWarning("I HATE People");
            }
        }
    }

    private void RecipeConstitution(ElementPreference elementPreference, Build otherBuild)
    {

        DoLikeIngredient(otherBuild.item.recipe.ingredient1);
        DoLikeIngredient(otherBuild.item.recipe.ingredient2);
        DoLikeIngredient(otherBuild.item.recipe.ingredient3);

        void DoLikeIngredient(Recipe.Ingredient ingredient)
        {
            if (ingredient.ingredientType == null)
                return;

            if (elementPreference.objectLike.ToString() == ingredient.ingredientType.Name)
            {
                if (elementPreference.likeDislike == LikeDislike.Like)
                    ++positiveMood;
                else
                    ++negativeMood;
            }
        }
    }

    public void Hover()
    {
        
    }

    public void UnHover()
    {
        
    }

    public void Interact()
    {
        _dialogue.EndDiag += End;
        Missy.isDialogOpen = true;
        PlayerController.Instance.DisablePlayer();
        _dialogue.NextDialog();
    }
    
    private void End(DialogueData dialog)
    {
        if (dialog.isDisplay == true)
        {                   
            dialog.isDisplay = false;
            InteractionManager.Instance.InteruptInteraction();
        }
        
    }

    public void EndInteract()
    {
        Missy.isDialogOpen = false;
        PlayerController.Instance.EnablePlayer();
        _dialogue.EndDiag -= End;
    }

    public InteractMode GetInteractMode()
    {
        throw new NotImplementedException();
    }
}
