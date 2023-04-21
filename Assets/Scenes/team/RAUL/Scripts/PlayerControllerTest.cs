using Mirror;
using UnityEngine;
using Cinemachine;

public class PlayerControllerTest : NetworkBehaviour
{
    public float moveSpeed = 5f;

    private Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();

        if (isLocalPlayer)
        {
            CinemachineVirtualCamera virtualCamera = FindObjectOfType<CinemachineVirtualCamera>();
            if (virtualCamera != null)
            {
                virtualCamera.Follow = transform;
            }
        }
    }

    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0, vertical) * moveSpeed * Time.deltaTime;
        rb.MovePosition(rb.position + movement);
    }
}
