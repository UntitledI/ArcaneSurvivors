using UnityEngine;
using Mirror;

public class ArrowAbility : NetworkBehaviour
{
    public GameObject swordPrefab; // Set this to your sword prefab in the Unity editor
    public float throwForce = 10f; // Set this to how hard you want to throw the sword
    public AudioClip throwSound;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("No AudioSource found on this object. Please add one.");
        }
    }

    void Update()
    {
        if (!isLocalPlayer) return; // If it's not the local player, don't check for input

        // Checking for '1' key press
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            CmdThrowSword();
        }
    }

    [Command]
    void CmdThrowSword()
    {
        // Calculate the start position and direction for the sword
        Vector3 swordStartPosition = transform.position + transform.forward;
        Vector3 swordDirection = transform.forward;

        // Tell the server to create the sword and throw it
        RpcThrowSword(swordStartPosition, swordDirection);
    }

    [ClientRpc]
    void RpcThrowSword(Vector3 startPosition, Vector3 direction)
    {
        // Play throw sound
        if (throwSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(throwSound);
        }

        // Create a new sword at the start position and throw it in the direction
        GameObject sword = Instantiate(swordPrefab, startPosition, Quaternion.identity);
        Rigidbody swordRigidbody = sword.GetComponent<Rigidbody>();
        if (swordRigidbody != null)
        {
            swordRigidbody.AddForce(direction * throwForce, ForceMode.Impulse);
        }
        else
        {
            Debug.LogError("Sword prefab must have a Rigidbody component for the throwing to work.");
        }

        // If you want the sword to be destroyed after some time, uncomment this line
        Destroy(sword, 5f); // The sword will be destroyed after 5 seconds
    }
}
