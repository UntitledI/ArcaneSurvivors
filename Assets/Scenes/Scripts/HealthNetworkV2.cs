using UnityEngine;
using Mirror;
using UnityEngine.UI;
using TMPro;

public class HealthNetworkV2 : NetworkBehaviour
{
    [SerializeField]
    private int startingHealth = 100;
    [SerializeField]
    private AudioClip deathSound;
    [SerializeField]
    private Slider healthBar; // Assign this in the inspector
    [SerializeField]
    private TextMeshProUGUI healthText;

    private NetworkStartPosition[] spawnPositions;
    private Animator animator;
    private AudioSource audioSource;

    [SyncVar(hook = nameof(OnHealthChanged))]
    private int currentHealth;

    void Start()
    {
        spawnPositions = FindObjectsOfType<NetworkStartPosition>();

        currentHealth = startingHealth;
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Initialize Animator's Health parameter to startingHealth
        animator.SetInteger("Health", startingHealth);

        // Ensure health UI is updated right at the start
        OnHealthChanged(0, currentHealth);

        Debug.Log("Number of NetworkStartPositions: " + spawnPositions.Length);

    }

    public void TakeDamage(int amount)
    {
        if (!isServer) return; // Only the server should handle damage application

        currentHealth -= amount;
        animator.SetInteger("Health", currentHealth); // Update Animator's Health parameter

        // If the health drops to 0 or below, handle death
        if (currentHealth <= 0)
        {
            HandleDeath();
        }
    }

    void HandleDeath()
    {
        // Play death animation
        animator.SetInteger("Health", 0); // Set Health parameter to 0

        // Play death sound
        if (deathSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(deathSound);
        }

        // If this is the player, respawn
        if (gameObject.CompareTag("Player"))
        {
            RpcRespawn();
        }
        else
        {
            // If it's not a player, just destroy the object
            NetworkServer.Destroy(gameObject);
        }
    }

    [ClientRpc]
    void RpcRespawn()
    {
        if (isLocalPlayer)
        {
            // Respawn player at the respawn point after waiting for the death animation
            StartCoroutine(RespawnAfterDelay(3)); // 3 seconds delay
            Debug.Log("Respawn sequence initiated");
        }
    }

System.Collections.IEnumerator RespawnAfterDelay(float delay)
{
    yield return new WaitForSeconds(delay);
    NetworkStartPosition chosenSpawnPosition = spawnPositions[Random.Range(0, spawnPositions.Length)];
    transform.position = chosenSpawnPosition.transform.position;
    Debug.Log("Respawning player at: " + chosenSpawnPosition.transform.position);
    currentHealth = startingHealth;
    animator.SetInteger("Health", startingHealth); // Reset Health parameter to startingHealth
}



    // SyncVar hook to handle health changes
    void OnHealthChanged(int oldHealth, int newHealth)
    {
        Debug.Log("OnHealthChanged called. New Health: " + newHealth);

        if (animator == null)
        {
            animator = GetComponent<Animator>();
        }

        if (animator == null)
        {
            Debug.LogError("Animator is still null after attempting to find it.");
            return;
        }
        // Update Animator's Health parameter
        animator.SetInteger("Health", newHealth);
        // Update health bar and text
        if (healthBar == null) Debug.LogError("HealthBar is null"); // Slider value is between 0 and 1
        else healthBar.value = newHealth / (float)startingHealth;

        if (healthText == null) Debug.LogError("HealthText is null");
        else healthText.text = newHealth + "/" + startingHealth;
    }

    public int GetCurrentHealth()
    {
        return currentHealth;
    }
}
