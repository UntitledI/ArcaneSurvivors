using UnityEngine;
using Mirror;

public class SwordDamage : NetworkBehaviour
{
    public int damage = 10; // Set this to the amount of damage you want the sword to deal

    void OnCollisionEnter(Collision collision)
    {
        if (!isServer) return; // Damage calculation should only happen on the server

        var hit = collision.gameObject;
        var health = hit.GetComponent<Health>(); // Assumes a 'Health' script is attached to player and enemies

        if (health != null)
        {
            health.TakeDamage(damage); // Call a method to decrease health
        }

        // Destroy the sword after it hits something
        NetworkServer.Destroy(gameObject);
    }
}
