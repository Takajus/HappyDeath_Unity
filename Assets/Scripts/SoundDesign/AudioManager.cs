using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;
    public AK.Wwise.Event Craft;
    public AK.Wwise.Event Dig;
    public AK.Wwise.Event Day_Music;
    public AK.Wwise.Event Footsteps_Ghost;
    public AK.Wwise.Event Footsteps_Grass;
    public AK.Wwise.Event Menu_Music;
    public AK.Wwise.Event MetalGate_Close;
    public AK.Wwise.Event MetalGate_Open;
    public AK.Wwise.Event Place_Tombstone;
    public AK.Wwise.Event Take_Flower;
    public AK.Wwise.Event Take_Rock;
    public AK.Wwise.Event Take_TreeBranch;
    public AK.Wwise.State Pattern_Day;
    public AK.Wwise.State Pattern_Night;
    
  
    //public AK.Wwise.Event Footsteps_Dirt;
    // public AK.Wwise.Event Night_Music;
    // public AK.Wwise.Event Footsteps_Stone;
    // public AK.Wwise.Event Footsteps_Wood;




    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        FindObjectOfType<AudioManager>().PlaySound(Day_Music);
    }

    public void PlaySound(AK.Wwise.Event wwise_event)
    {
            wwise_event.Post(gameObject);
        //How to play sound in scripts:
        //FindObjectOfType<AudioManager>().PlaySound("name of the sound");
    }

    public void StopSound(AK.Wwise.Event wwise_event)
    {
        //How to stop sound in scripts:
        //FindObjectOfType<AudioManager>().StopSound("name of the sound");
    }

    private void Update()
    {


        //if (Input.GetKeyUp(KeyCode.Escape))
        //{
        //    FindObjectOfType<AudioManager>().PlaySound(Craft);
        //    //PlaySound(Craft);
        //}
    }

    //How to play sound in scripts:
    //FindObjectofType<AudioManager>().Play("name of the sound");
}