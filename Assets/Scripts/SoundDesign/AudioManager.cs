using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
    }

    public void Start()
    {
        PlaySound("wiisport_theme");
    }

    public void PlaySound(string name)
    {
        AkSoundEngine.PostEvent(name, gameObject);
        //How to play sound in scripts:
        //FindObjectOfType<AudioManager>().PlaySingleSound("name of the sound");
    }

    public void StopSound(string name)
    {
        //How to stop sound in scripts:
        //FindObjectOfType<AudioManager>().StopSingleSound("name of the sound");
    }
 
    //How to play sound in scripts:
    //FindObjectofType<AudioManager>().Play("name of the sound");
}