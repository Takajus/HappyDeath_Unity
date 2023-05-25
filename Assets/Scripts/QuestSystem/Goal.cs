using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    public string Description {  get; set; }
    public bool Completed { get; set; }
    public int CurrentAmount { get; set; }
    public int RequreAmount { get; set; }



    public virtual void Init()
    {
        // default int stuff
    }

    public void Evaluate()
    {
        if(CurrentAmount >= RequreAmount)
        {
            Complete();
        }
    }
    public void Complete()
    {
        Completed = true;
    }

}
