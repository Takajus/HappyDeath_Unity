using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundManager : MonoBehaviour
{
    // Les références aux événements sonores dans Wwise
    public AK.Wwise.Event soundEvent; // Événement sonore
    public AK.Wwise.Event musicEvent; // Événement musical

    // Méthode pour lancer un son
    public void PlaySound()
    {
        if (soundEvent != null)
        {
            soundEvent.Post(gameObject); // Lancer l'événement sonore
        }
    }

    // Méthode pour lancer de la musique
    public void PlayMusic()
    {
        if (musicEvent != null)
        {
            musicEvent.Post(gameObject); // Lancer l'événement musical
        }
    }

    // Méthode pour arrêter tous les sons et la musique
    public void StopAllAudio()
    {
        /*AK.Wwise.EventManager.StopAll(gameObject);*/ // Arrêter tous les événements sonores et musicaux
    }
}