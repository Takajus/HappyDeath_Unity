using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AK.Wwise.Event Flower;


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

    public void Update()
    {
        if (Input.GetKeyUp(KeyCode.X))
        {
            PlaySound(Flower);
        }
    }

    public void PlaySound(AK.Wwise.Event wwise_event)
    {
        wwise_event.Post(gameObject);
        //How to play sound in scripts:
        //FindObjectOfType<AudioManager>().PlaySound("name of the sound");
    }

    public void StopSound(string name)
    {
        //How to stop sound in scripts:
        //FindObjectOfType<AudioManager>().StopSound("name of the sound");
    }
 
    //How to play sound in scripts:
    //FindObjectofType<AudioManager>().Play("name of the sound");
}