using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaySound : MonoBehaviour
{
    public float detectionRadius = 5f; // Radius for detecting the player
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        // Check if player is within detection radius
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null && Vector3.Distance(transform.position, player.transform.position) <= detectionRadius)
        {
            // Check if the 'E' key is pressed
            if (Input.GetKeyDown(KeyCode.E))
            {
                // Play the sound
                audioSource.Play();
            }
        }
    }
}
