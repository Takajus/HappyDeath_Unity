using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


public class Resident : MonoBehaviour
{
    public ResidentData ResidentData;

    private GameObject modelSlot, model;
    private bool placeIsCheck = false;

    [SerializeField] private float detectionRadius = 5f;
    //[SerializeField] private LayerMask residentLayer, placeLayer, objectLayer;
    private int residentAmount = 0;
    float negativeMood = 0f, positiveMood = 0f;

    private void Awake()
    {
        // TODO: Modifier la list pour utiliser celle de la DataBase
        MoodManager.residentList.Add(this);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;    
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    private void OnEnable()
    {
        if(!modelSlot)
            modelSlot = transform.Find("Model").gameObject;

        if(!model)
            model = Instantiate(ResidentData.prefab, modelSlot.transform);
        if (!model.activeInHierarchy)
            model.SetActive(true);

        negativeMood = 0f;
        positiveMood = 0f;
        residentAmount = 0;
        placeIsCheck = false;

        if (detectionRadius < 1f)
            return;
        
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

                        if (elementPreference.すき == Race.General)
                        {
                            AmountCalcul(elementPreference);
                        }

                        if (elementPreference.すき == otherResident.race)
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
                        /*PickUpItem otherItem = col.gameObject.GetComponent<PickUpItem>();
                        if(!otherItem) break;
                        
                        if (elementPreference.objectLike.ToString() == otherItem.gameObject.name)
                        {
                            if (elementPreference.likeDislike == LikeDislike.Like)
                            {
                                ++positiveMood;
                                // Debug.Log("I LIKE this Object");
                            }
                            else
                            {
                                ++negativeMood;
                                // Debug.LogWarning("I HATE this Object");
                            }
                        }*/
                        
                        Build otherBuild = col.gameObject.GetComponent<Build>();
                        if(!otherBuild) break;

                        if (RecipeConstitution(elementPreference, otherBuild))
                        {
                            if (elementPreference.likeDislike == LikeDislike.Like)
                            {
                                ++positiveMood;
                                // Debug.Log("I LIKE this Object");
                            }
                            else
                            {
                                ++negativeMood;
                                // Debug.LogWarning("I HATE this Object");
                            }
                        }
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
        
        ResidentData.mood = (positiveMood/nbBonus) - (negativeMood/nbBonus);

        // Debug.Log(Resident.name + " - Humeur : " + Resident.mood);
    }

    private void OnDisable()
    {
        if (!model.activeInHierarchy)
            model.SetActive(false);
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

    private bool RecipeConstitution(ElementPreference elementPreference, Build otherBuild)
    {
        if (elementPreference.objectLike.ToString() == otherBuild.item.recipe.ingredient1.ingredientType.Name &&
            elementPreference.objectLike.ToString() == otherBuild.item.recipe.ingredient2.ingredientType.Name &&
            elementPreference.objectLike.ToString() == otherBuild.item.recipe.ingredient3.ingredientType.Name)
        {
            
        }
        return true;
    }
}
