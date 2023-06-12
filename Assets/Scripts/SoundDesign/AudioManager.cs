using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AK.Wwise.Event Craft;
    public AK.Wwise.Event Day_Music;
    public AK.Wwise.Event Dig;
    public AK.Wwise.Event Footsteps_Dirt;
    public AK.Wwise.Event Footsteps_Ghost;
    public AK.Wwise.Event Footsteps_Grass;
    public AK.Wwise.Event Footsteps_Stone;
    public AK.Wwise.Event Footsteps_Wood;
    public AK.Wwise.Event Menu_Music;
    public AK.Wwise.Event MetalGate_Close;
    public AK.Wwise.Event MetalGate_Open;
    public AK.Wwise.Event Place_Tombstone;
    public AK.Wwise.Event Take_Flower;
    public AK.Wwise.Event Take_Rock;
    public AK.Wwise.Event Take_TreeBranch;


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