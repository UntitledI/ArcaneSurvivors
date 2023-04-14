using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
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
<<<<<<< HEAD
    //Jumping
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    private Animator animator;



    void Start()
    {
        trueSpeed = walkSpeed;
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;    

    }

    // Update is called once per frame
    void Update()
    {

        isGrounded = Physics.CheckSphere(transform.position, .1f, 1);
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

        //Moves the character in the direction of the camera
        if (direction.magnitude >= 0.1f)
=======
=======
>>>>>>> a0fce318 (readded Character)
=======
    //Essentials
>>>>>>> 2dd69085 (Animated Player)
    public CharacterController controller;
    public Transform cam;
    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> eff5772a (Player Movement repush)
    public Animator animator;
=======
    //Movement
    public float walkSpeed;
    public float sprintSpeed;
    bool sprinting;
    float trueSpeed;

=======
>>>>>>> 58e9032b (Polished Player Movement/Camera)
    //Jumping
    public float jumpHeight;
    public float gravity;
    bool isGrounded;
    Vector3 velocity;

    private Animator animator;
>>>>>>> 2dd69085 (Animated Player)

    void Start()
    {
        trueSpeed = walkSpeed;
        animator = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

<<<<<<< HEAD
=======
>>>>>>> a0fce318 (readded Character)
=======
>>>>>>> eff5772a (Player Movement repush)
    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(transform.position, .1f, 1);
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
<<<<<<< HEAD
        //Moves the character in the direction of the camera
<<<<<<< HEAD
        if(direction.magnitude >= 0.1f)
>>>>>>> 4a281c55 (readded Character)
=======
        if (direction.magnitude >= 0.1f)
>>>>>>> b6e6f083 (Player Movement repush)
=======

        //Moves the character in the direction of the camera
        if(direction.magnitude >= 0.1f)
>>>>>>> a0fce318 (readded Character)
=======
=======

>>>>>>> 2dd69085 (Animated Player)
        //Moves the character in the direction of the camera
        if (direction.magnitude >= 0.1f)
>>>>>>> eff5772a (Player Movement repush)
        {
            //calculates the target angle the character is facing with the camera
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            //smoothes out the rate at which the character rotates
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            //executes the rotation of the character
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            //moves the character in the direction of the camera
            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            //Moves the character through characterController object
<<<<<<< HEAD
<<<<<<< HEAD
<<<<<<< HEAD
=======
>>>>>>> 2dd69085 (Animated Player)
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
<<<<<<< HEAD
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
    }

=======
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
=======
>>>>>>> 2dd69085 (Animated Player)
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
    }
>>>>>>> 4a281c55 (readded Character)
=======
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
    }
>>>>>>> a0fce318 (readded Character)
}
