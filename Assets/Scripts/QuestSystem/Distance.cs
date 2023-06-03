using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Distance : MonoBehaviour
{
    //Reference to point 
    [SerializeField]
    private Transform pointA;
   
    //calcuated disatnce value 
    private float distance;

    
    // Update is called once per frame
    void Update()
    {

     distance = (pointA.transform.position - transform.position).magnitude;
        Debug.Log(distance);

        


    }
}
