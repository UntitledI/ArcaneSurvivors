using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoisonAura : MonoBehaviour
{
    [SerializeField] private GameObject particle;
    [SerializeField] private float cooldown = 10f; // The cooldown time in seconds

    private float lastActivatedTime = -Mathf.Infinity;

    private void LoadParticleSystem()
    {
        // Check if the cooldown has passed
        if (Time.time >= lastActivatedTime + cooldown)
        {
            GameObject particleInstance = Instantiate(particle, transform.position, transform.rotation);
            ParticleSystem PoisonMist = particleInstance.GetComponent<ParticleSystem>();
            if (PoisonMist != null)
            {
                PoisonMist.Play();
            }

            lastActivatedTime = Time.time; // Set the last activated time to now
        }
    }
    private void Update() //will always check once initialize
    {
        if (Input.GetKeyDown(KeyCode.Alpha4)) //change input to your desired key
        {
            LoadParticleSystem(); //call ability function
        }
    }
}