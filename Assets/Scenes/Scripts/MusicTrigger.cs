using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicTrigger : MonoBehaviour
{
    public AudioSource townMusic;
    public AudioSource tavernAmbience;
    private AudioSource currentAudioSource;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Check which trigger zone the player has entered
            if (gameObject.name == "Town")
            {
                SwitchMusic(townMusic);
            }
            else if (gameObject.name == "SF_Bld_House_Tavern_01")
            {
                SwitchMusic(tavernAmbience);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            // Stop the current music when the player leaves the trigger zone
            if (currentAudioSource != null && currentAudioSource.isPlaying)
            {
                currentAudioSource.Stop();
                currentAudioSource = null;
            }
        }
    }

    private void SwitchMusic(AudioSource newAudioSource)
    {
        // Stop the current music
        if (currentAudioSource != null && currentAudioSource.isPlaying)
        {
            currentAudioSource.Stop();
        }

        // Start the new music
        currentAudioSource = newAudioSource;
        currentAudioSource.Play();
    }
}

