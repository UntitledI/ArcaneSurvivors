using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaSound : MonoBehaviour
{
    public float activationRadius = 5f; // Radius for activating the sound
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        // Set the volume to 0 initially, because the player is presumably not in range yet
        audioSource.volume = 0;
        audioSource.Play();  // Start playing the sound
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            float distance = Vector3.Distance(transform.position, player.transform.position);

            // Only adjust volume if player is within activation radius
            if (distance <= activationRadius)
            {
                // Normalize the distance between 0 and 1, then subtract it from 1 so closer is louder
                audioSource.volume = 1 - (distance / activationRadius);
            }
            else
            {
                // If the player is outside the activation radius, set volume to 0
                audioSource.volume = 0;
            }
        }
    }
}
