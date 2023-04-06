using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class Player : NetworkBehaviour
{
    public GameObject playerCamera;
    public GameObject playerModel;
    public GameObject playerUI;

    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        playerCamera.SetActive(true);
        playerModel.SetActive(true);
        playerUI.SetActive(true);
    }
}

public class Player_Controller : NetworkBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 5f;
    public float walkSpeed = 2f;
    public float jumpForce = 5f;
    public int maxJumps = 2;
    public int jumpStaminaCost = 5;
    public int sprintStaminaCostPerSecond = 3;
    public float dodgeRollDuration = 2f;
    public int dodgeRollStaminaCost = 10;
    private int attackComboCount = 0;
    private float lastAttackTime = 0f;
    private bool isDodgeRolling = false;
    private float dodgeRollTimer = 0f;
    private bool isGrounded = true;
    private int currentJumps = 0;
    private bool isSprinting = false;
    private float sprintStaminaTimer = 0f;
    private Rigidbody rb;
    private Animator animator;
    private StaminaManager staminaManager;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        staminaManager = GetComponent<StaminaManager>();
    }

    void Update()
    {
        if (isLocalPlayer == false)
        {
            return;
        }
        {
            HandleMovement();
            HandleJump();
            HandleSprint();
            HandleAttacks();
            HandleAbilities();
            HandleDodgeRoll();
        }
    }

    void HandleMovement()
    {
        if (isDodgeRolling) return;

        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 moveDirection = new Vector3(horizontalInput, 0, verticalInput).normalized;

        if (isSprinting)
        {
            transform.position += moveDirection * moveSpeed * Time.deltaTime;
        }
        else
        {
            transform.position += moveDirection * walkSpeed * Time.deltaTime;
        }

        if (moveDirection != Vector3.zero)
        {
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 10f);
        }
    }

    void HandleJump()
    {
        if (isDodgeRolling) return;

        if (Input.GetKeyDown(KeyCode.Space) && currentJumps < maxJumps && staminaManager.currentStamina >= jumpStaminaCost)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            currentJumps++;
            staminaManager.ReduceStamina(jumpStaminaCost);
            animator.SetTrigger("Jump");
        }
    }

    void HandleSprint()
    {
        if (isDodgeRolling) return;

        if (Input.GetKey(KeyCode.LeftShift) && staminaManager.currentStamina > 0)
        {
            isSprinting = true;

            sprintStaminaTimer += Time.deltaTime;
            if (sprintStaminaTimer >= 1f)
            {
                staminaManager.ReduceStamina(sprintStaminaCostPerSecond);
                sprintStaminaTimer -= 1f;
            }
        }
        else
        {
            isSprinting = false;
            sprintStaminaTimer = 0f;
        }
    }

    void HandleAttacks()
    {
        if (isDodgeRolling) return;

        if (Input.GetMouseButtonDown(0))
        {
            animator.SetTrigger("StrongAttack");
        }
        else if (Input.GetMouseButtonDown(1))
        {
            attackComboCount++;
            lastAttackTime = Time.time;

            if (attackComboCount == 1)
            {
                animator.SetTrigger("SingleAttack1");
            }
            else if (attackComboCount == 2)
            {
                animator.SetTrigger("SingleAttack2");
            }
            else if (attackComboCount == 3)
            {
                animator.SetTrigger("SingleAttack3");
            }
            else if (attackComboCount == 4)
            {
                animator.SetTrigger("SingleAttack4");
                attackComboCount = 0;
            }
        }

        if (Time.time - lastAttackTime > 1f)
        {
            attackComboCount = 0;
        }
    }

    void HandleAbilities()
    {
        if (isDodgeRolling) return;

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // activate ability 1
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // activate ability 2
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // activate ability 3
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            // activate ability 4
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            // activate ability 5
        }
    }

    /*
    This function checks if the character is currently performing a dodge roll.
    If they are, it decreases the dodgeRollTimer by Time.deltaTime and checks if the timer has reached 0 or below.
    If it has, it sets the isDodgeRolling variable to false, resets the dodgeRollTimer to the value of dodgeRollDuration, and sets the “IsDodgeRolling” animator parameter to false.

    If the character is not currently performing a dodge roll, the function checks if they have enough stamina to perform a dodge roll by comparing their current stamina to the dodgeRollStaminaCost.
    If they don’t have enough stamina, the function returns early.

    Next, the function checks for double-tap input on the WASD keys to determine which direction to dodge roll in.
    If a double-tap is detected on one of these keys, it sets the dodgeDirection variable to the appropriate direction.

    Finally, if the dodgeDirection variable is not equal to Vector3.zero, meaning that a valid dodge roll input was detected,
    the function calls the ReduceStamina function on the staminaManager script to deplete the character’s stamina by the dodgeRollStaminaCost.
    It then sets the isDodgeRolling variable to true, sets the “IsDodgeRolling” animator parameter to true, and moves the character in the dodge direction by a fixed amount.
    */
    void HandleDodgeRoll()
    {
        if (isDodgeRolling)
        {
            dodgeRollTimer -= Time.deltaTime;
            if (dodgeRollTimer <= 0)
            {
                isDodgeRolling = false;
                dodgeRollTimer = dodgeRollDuration;
                animator.SetBool("IsDodgeRolling", false);
            }
            return;
        }

        if (staminaManager.currentStamina < dodgeRollStaminaCost) return;

        Vector3 dodgeDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.W) && Input.GetKeyDown(KeyCode.W))
        {
            dodgeDirection = transform.forward;
        }
        else if (Input.GetKeyDown(KeyCode.A) && Input.GetKeyDown(KeyCode.A))
        {
            dodgeDirection = -transform.right;
        }
        else if (Input.GetKeyDown(KeyCode.S) && Input.GetKeyDown(KeyCode.S))
        {
            dodgeDirection = -transform.forward;
        }
        else if (Input.GetKeyDown(KeyCode.D) && Input.GetKeyDown(KeyCode.D))
        {
            dodgeDirection = transform.right;
        }

        if (dodgeDirection != Vector3.zero)
        {
            staminaManager.ReduceStamina(dodgeRollStaminaCost);
            isDodgeRolling = true;
            animator.SetBool("IsDodgeRolling", true);
            transform.position += dodgeDirection * 2f;
        }
    }
}
