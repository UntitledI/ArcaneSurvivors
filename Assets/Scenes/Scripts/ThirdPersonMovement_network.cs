using Mirror;
using UnityEngine;

public class ThirdPersonMovement_network : NetworkBehaviour
{
    public CharacterController controller;
    public float walkSpeed = 2f;
    public float sprintSpeed = 5f;
    public float jumpHeight = 1f;
    public float gravity = -9.8f;
    public float rotationSpeed = 0.1f;
    public Transform cam;

    private float speed;
    private Vector3 velocity;
    private bool isGrounded;
    private float groundDistance = 0.4f;
    public Transform groundCheck;
    public LayerMask groundMask;

    private Animator animator;
    private FootstepSounds footstepSounds;

    private void Start()
    {
        if(controller == null)
        {
            controller = GetComponent<CharacterController>();
        }

        animator = GetComponent<Animator>();
        footstepSounds = GetComponent<FootstepSounds>();
    }

    [Command]
    void CmdJump()
    {
        if (isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            RpcJump(velocity.y);
        }
    }

    [ClientRpc]
    void RpcJump(float velocityY)
    {
        velocity.y = velocityY;
    }


    private void Update()
    {
        if (!isLocalPlayer)
        {
            return;
        }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 moveDirection = new Vector3(x, 0f, z);
        Vector3 move = cam.right * moveDirection.x + cam.forward * moveDirection.z;
        move.y = 0f;  // This ensures the vertical component is zero

        speed = Input.GetKey(KeyCode.LeftShift) ? sprintSpeed : walkSpeed;

        controller.Move(move * speed * Time.deltaTime);

        // Rotate character to face movement direction
        if (move != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(move, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, rotationSpeed);
        }

        if (Input.GetButtonDown("Jump"))
        {
           CmdJump();
        }

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

        // Update the speed in the animator
        animator.SetFloat("Speed", move.magnitude * speed);

        // If the character has stopped moving, stop the footstep sounds
        if (controller.velocity.magnitude == 0f)
        {
            footstepSounds.CancelInvoke(nameof(footstepSounds.PlayFootstep));
        }
        // If the character has started moving, start the footstep sounds
        else if (!footstepSounds.IsInvoking(nameof(footstepSounds.PlayFootstep)))
        {
            footstepSounds.InvokeRepeating(nameof(footstepSounds.PlayFootstep), 0, Random.Range(footstepSounds.MinInterval, footstepSounds.MaxInterval));
        }


    // This part checks if the player is moving and plays footstep sound
    if (move.magnitude >= 0.5f && footstepSounds.CanPlaySound())
    {
        footstepSounds.PlayFootstep();
        footstepSounds.ResetTimer();

        Debug.Log("Trying to play footstep sound"); // Inside condition
    }

    }
}


