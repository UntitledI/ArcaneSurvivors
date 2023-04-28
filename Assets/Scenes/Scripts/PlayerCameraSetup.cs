using UnityEngine;
using Mirror;
using Cinemachine;

public class PlayerCameraSetup : NetworkBehaviour
{
    public GameObject cameraPrefab;
    private CinemachineFreeLook freeLookCamera;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();

        // Instantiate the camera prefab
        GameObject cameraInstance = Instantiate(cameraPrefab);

        // Assign the camera to the player
        freeLookCamera = cameraInstance.GetComponentInChildren<CinemachineFreeLook>();
        freeLookCamera.Follow = transform;
        freeLookCamera.LookAt = transform;

        // Assign the camera reference to the ThirdPersonMovement_network script
        ThirdPersonMovement_network movementScript = GetComponent<ThirdPersonMovement_network>();
        movementScript.cam = cameraInstance.transform;
    }
}
