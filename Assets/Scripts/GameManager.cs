using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        uint eventID = AkSoundEngine.PostEvent("Pattern_Day", gameObject);
        Day();
    }

    static public void Night()
    {
        AkSoundEngine.SetState("Pattern_Night", "NightMusic");
    }
    
    static public void Day()
    {
        AkSoundEngine.SetState("Pattern_Day", "DayMusic");
    }
    // Update is called once per frame
    
}
