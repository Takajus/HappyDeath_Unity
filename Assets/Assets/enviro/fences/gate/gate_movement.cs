using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gate_movement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.GetComponent<Animator>().SetBool("isGateClacking", false);
    }

    public void checkGate(int isGateClapTrue)
    {
        int isGateTrue = gameObject.GetComponent<Animator>().GetInteger("isGateClacking");

        //si le bool est vrai (aka si l'animator joue l'animation de claquement...
        if (isGateTrue == 1)
        {
            gameObject.GetComponent<Animator>().SetInteger("isGateClacking", 1);
        }
        
        //s'il joue l'idle, une chance sur cinq de lancer l'animation de claquement
        else
        {
            int isGateGonnaClap = Random.Range(0, 5);

            if (isGateGonnaClap == 0)
            {
                gameObject.GetComponent<Animator>().SetInteger("isGateClacking", 1);
            }
            else
            {
                gameObject.GetComponent<Animator>().SetInteger("isGateClacking", 0);
            }
        }
    }
}
