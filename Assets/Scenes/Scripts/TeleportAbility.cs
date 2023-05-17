using UnityEngine;
using Mirror;

public class TeleportAbility : NetworkBehaviour
{
    public float teleportDistance = 5f; // Set this to the distance you want to teleport
    public AudioClip teleportSound;
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
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CmdTeleport();
        }
    }

    [Command]
    void CmdTeleport()
    {
        Vector3 teleportDirection = transform.forward; // Change this if you want to teleport in a different direction
        Vector3 newPosition = transform.position + teleportDirection * teleportDistance;
        RpcTeleport(newPosition);
    }

    [ClientRpc]
    void RpcTeleport(Vector3 newPosition)
    {
        // Play teleport sound
        if (teleportSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(teleportSound);
        }

        // Teleport the player
        transform.position = newPosition;
    }
}
