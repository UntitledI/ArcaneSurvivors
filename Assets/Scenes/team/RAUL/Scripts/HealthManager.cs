using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;


/*
This script uses the SyncVar attribute to synchronize the values of the variables between the server and all clients.
The TakeDamage function has been replaced with a CmdTakeDamage function that is marked with the Command attribute.
This means that this function can only be called by a client and will be executed on the server.

The Update and OnGUI functions now include an if statement that checks if the character is controlled by the local player.
If it is, these functions will run their code as normal.

The Revive function has been replaced with a CmdRevive function that is also marked with the Command attribute.
This means that this function can also only be called by a client and will be executed on the server.
*/
public class HealthManager : NetworkBehaviour
{
    [SyncVar]
    public int defaultHealth = 100;
    [SyncVar]
    public int healthRegenRate = 1;
    [SyncVar]
    public float healthRegenDelay = 1f;
    [SyncVar]
    private int currentHealth;
    [SyncVar]
    private float healthRegenTimer;
    [SyncVar]
    private bool isDead = false;

    void Start()
    {
        currentHealth = defaultHealth;
        healthRegenTimer = healthRegenDelay;
    }

    void Update()
    {
        if (isLocalPlayer)
        {
            RegenerateHealth();
        }
    }

    void OnGUI()
    {
        if (isLocalPlayer)
        {
            DisplayHealth();
            if (isDead)
            {
                if (GUI.Button(new Rect(10, 40, 100, 20), "Revive"))
                {
                    CmdRevive();
                }
            }
        }
    }

    [Command]
    public void CmdTakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        healthRegenTimer = healthRegenDelay; // reset the health regeneration timer
    }

    void RegenerateHealth()
    {
        if (currentHealth < defaultHealth)
        {
            healthRegenTimer -= Time.deltaTime;
            if (healthRegenTimer <= 0)
            {
                currentHealth += healthRegenRate;
                healthRegenTimer = healthRegenDelay;
            }
        }
    }

    void DisplayHealth()
    {
        GUI.Label(new Rect(10, 10, 100, 20), "Health: " + currentHealth);
    }

    void Die()
    {
        // Add code here to handle the death of the character
        Debug.Log(gameObject.name + " has died.");
        isDead = true;
    }

    [Command]
    void CmdRevive()
    {
        currentHealth = defaultHealth;
        isDead = false;
    }
}