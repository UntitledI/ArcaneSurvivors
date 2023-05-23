using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MeteorSpellNetwork : NetworkBehaviour
{
    public GameObject player;
    public GameObject meteorPrefab;
    public GameObject ring;
    public float distance = 10f;
    private bool isCasting = false;
    public float spawnHeight = 10f; // Height above targeting ring to spawn meteor
    private Vector3 originalCameraPosition; // original camera position before casting mode
    private Quaternion originalCameraRotation; // original camera rotation before casting mode
    public float castingCameraHeight = 20f; // height of camera when in casting mode
    public AudioSource audioSource; // reference to the audio source component

    void Start()
    {
        originalCameraPosition = Camera.main.transform.position;
        originalCameraRotation = Camera.main.transform.rotation;
    }

    void Update()
    {
        // Make sure the code only runs for the local player
        if (!isLocalPlayer) return;

        // If the 2 key is pressed, activate the casting state and display the targeting ring
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            isCasting = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            ring.SetActive(true);
            // Set camera to top-down view at casting height
            Camera.main.transform.position = new Vector3(player.transform.position.x, castingCameraHeight, player.transform.position.z);
            Camera.main.transform.rotation = Quaternion.Euler(90f, 0f, 0f);
        }

        // While casting, move the targeting ring to the location of the cursor on a hit point
        if (isCasting)
        {
            Vector3 cursorPosition = new Vector3(Input.mousePosition.x, 0f, Input.mousePosition.y);
            Ray cursorRay = Camera.main.ScreenPointToRay(cursorPosition);
            if (Physics.Raycast(cursorRay, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 spawnPosition = hit.point + Vector3.up * 0.05f;
                ring.transform.position = spawnPosition;
            }
        }

        // If the left mouse button is pressed and casting is active, spawn a meteor at the targeting ring's position
        if (Input.GetButtonDown("Fire2") && isCasting)
        {
            isCasting = false;
            Cursor.lockState = CursorLockMode.Locked;
            ring.SetActive(false);
            Vector3 spawnPosition = new Vector3(ring.transform.position.x, spawnHeight, ring.transform.position.z);
            Quaternion spawnRotation = Quaternion.Euler(90f, 0f, 0f); // rotate meteorPrefab to face downwards

            CmdSpawnMeteorAndPlaySound(spawnPosition, spawnRotation); // call the command

            // Reset camera to original position and rotation
            Camera.main.transform.position = originalCameraPosition;
            Camera.main.transform.rotation = originalCameraRotation;
        }

        // If the escape key is pressed, deactivate the casting state and hide the targeting ring
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isCasting = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            ring.SetActive(false);
            // Reset camera to original position and rotation
            Camera.main.transform.position = originalCameraPosition;
            Camera.main.transform.rotation = originalCameraRotation;
        }
    }

    [Command]
    void CmdSpawnMeteorAndPlaySound(Vector3 spawnPosition, Quaternion spawnRotation)
    {
        GameObject meteor = Instantiate(meteorPrefab, spawnPosition, spawnRotation);
        NetworkServer.Spawn(meteor);
        // Play the audio clip
        RpcPlayMeteorSound();
    }

    [ClientRpc]
    public void RpcPlayMeteorSound()
    {
        audioSource.Play();
    }
}