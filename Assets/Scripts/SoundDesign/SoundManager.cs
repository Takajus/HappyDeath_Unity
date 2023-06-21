using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    // Les r�f�rences aux �v�nements sonores dans Wwise
    public AK.Wwise.Event soundEvent; // �v�nement sonore
    public AK.Wwise.Event musicEvent; // �v�nement musical

    // M�thode pour lancer un son
    public void PlaySound()
    {
        if (soundEvent != null)
        {
            soundEvent.Post(gameObject); // Lancer l'�v�nement sonore
        }
    }

    // M�thode pour lancer de la musique
    public void PlayMusic()
    {
        if (musicEvent != null)
        {
            musicEvent.Post(gameObject); // Lancer l'�v�nement musical
        }
    }

    // M�thode pour arr�ter tous les sons et la musique
    public void StopAllAudio()
    {
        /*AK.Wwise.EventManager.StopAll(gameObject);*/ // Arr�ter tous les �v�nements sonores et musicaux
    }
}