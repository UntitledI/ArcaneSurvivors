using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ThirdPersonMovement_network : NetworkBehaviour
{
    //Essentials
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    //Movement
    public float walkSpeed;
    public float sprintSpeed;
    bool sprinting;
    float trueSpeed;
    public float sensitivity = 150f;
    //Jumping
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    private Animator animator;

    // Ground Layer
    public LayerMask groundLayer;

<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 29a514fa (Modifying network script to call RPC Client correctly and initlizize animator)
    void Awake()
    {
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }
<<<<<<< HEAD
=======
>>>>>>> 9e9d7b45 (Created networking scripts of player movement and combat animations)
=======
>>>>>>> 29a514fa (Modifying network script to call RPC Client correctly and initlizize animator)
    void Start()
    {
        trueSpeed = walkSpeed;
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isLocalPlayer) return;

        // Updated isGrounded check
        isGrounded = Physics.CheckSphere(transform.position, .1f, groundLayer);
        animator.SetBool("IsGrounded", isGrounded);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -1;
        }

        //Enables Sprinting
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            trueSpeed = sprintSpeed;
            sprinting = true;
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            trueSpeed = walkSpeed;
            sprinting = false;
        }
        //retains animations positions and angles
        animator.transform.localPosition = Vector3.zero;
        animator.transform.localEulerAngles = Vector3.zero;
        //retrieves cursor coordinate input
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

<<<<<<< HEAD
<<<<<<< HEAD

=======
>>>>>>> 9e9d7b45 (Created networking scripts of player movement and combat animations)
=======

>>>>>>> 29a514fa (Modifying network script to call RPC Client correctly and initlizize animator)
        //Moves the character in the direction of the camera
        if (direction.magnitude >= 0.1f)
        {
            //calculates the target angle the character is facing with the camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + transform.eulerAngles.y;
<<<<<<< HEAD
<<<<<<< HEAD

=======
>>>>>>> 9e9d7b45 (Created networking scripts of player movement and combat animations)
=======

>>>>>>> 29a514fa (Modifying network script to call RPC Client correctly and initlizize animator)
            //smoothes out the rate at which the character rotates
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //executes the rotation of the character
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //moves the character in the direction of the camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //Moves the character through characterController object
            controller.Move(moveDir.normalized * trueSpeed * Time.deltaTime);

            //flags to determine when player is walking/sprinting
            if (sprinting == true)
            {
                animator.SetFloat("Speed", 2);
            }
            else
            {
                animator.SetFloat("Speed", 1);
            }
        }
        else
        {
            animator.SetFloat("Speed", 0);
        }
        //Jumping
        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y = Mathf.Sqrt((jumpHeight * 10) * -2f * gravity);
        }

        if(velocity.y > -20)
        {
            velocity.y += (gravity * 10) * Time.deltaTime;
        }
        controller.Move(velocity * Time.deltaTime);
<<<<<<< HEAD
<<<<<<< HEAD
        // Sync animations for other players
        CmdSyncAnimations(animator.GetFloat("Speed"), isGrounded);
    }



    [Command]
        void CmdSyncAnimations(float speed, bool grounded)
        {
            RpcSyncAnimations(speed, grounded);
        }

        [ClientRpc]
        void RpcSyncAnimations(float speed, bool grounded)
        {
            if (isLocalPlayer) return;

            animator.SetFloat("Speed", speed);
            animator.SetBool("IsGrounded", grounded);
        }
}
=======
                // Sync animations for other players
                CmdSyncAnimations(animator.GetFloat("Speed"), isGrounded);
            }
=======
        // Sync animations for other players
        CmdSyncAnimations(animator.GetFloat("Speed"), isGrounded);
    }
>>>>>>> 29a514fa (Modifying network script to call RPC Client correctly and initlizize animator)



    [Command]
        void CmdSyncAnimations(float speed, bool grounded)
        {
            RpcSyncAnimations(speed, grounded);
        }

        [ClientRpc]
        void RpcSyncAnimations(float speed, bool grounded)
        {
            if (isLocalPlayer) return;

<<<<<<< HEAD
>>>>>>> 9e9d7b45 (Created networking scripts of player movement and combat animations)
=======
            animator.SetFloat("Speed", speed);
            animator.SetBool("IsGrounded", grounded);
        }
}
>>>>>>> 29a514fa (Modifying network script to call RPC Client correctly and initlizize animator)

