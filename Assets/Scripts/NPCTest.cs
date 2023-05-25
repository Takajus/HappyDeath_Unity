using System;
using UnityEngine;

public class NPCTest : MonoBehaviour
{
    [SerializeField] private NPCSample data;
    
    private GameObject modelSlot, NPCModel;

    private void Awake()
    {
        modelSlot = transform.Find("Model").gameObject;
    }

    private void OnEnable()
    {
        NPCModel = Instantiate(data.prefab, modelSlot.transform.position, data.prefab.transform.rotation, modelSlot.transform);
        // TODO: Attribuer les valeurs de data dans des variables ?
    }
    
    // TODO: mise en place du systeme d'humeur avec une premier test
    // TODO: Essayer de faire ça la plus generique possible
}
