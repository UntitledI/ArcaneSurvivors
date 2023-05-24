using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIChicken : MonoBehaviour
{
    public Animator animator;

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatisPlayer;

    public float health;

    //Patrolling

    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Attacking

    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public GameObject projectile;

    //States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

<<<<<<< HEAD
    private void Awake(){
=======
    //Audio for the Chicken
    public AudioClip chickenSound;
    private AudioSource audioSource;

    private void Awake(){
        audioSource = GetComponent<AudioSource>();
>>>>>>> 55069adfc5b60fb2e8800f284d1b656850938b0f
        animator = GetComponent<Animator>();
        player = GameObject.Find("PlayerObj").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update(){
        //check for the sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, whatisPlayer);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, whatisPlayer);

        if(!playerInSightRange && !playerInAttackRange){
            Patroling();
        }

        if(playerInSightRange && !playerInAttackRange){
            ChasePlayer();
        }

        if(playerInSightRange && playerInAttackRange){
            AttackPlayer();
        }
    }

    private void Patroling(){
        if(!walkPointSet){
            SearchWalkPoint();
        }

        if(walkPointSet){
            agent.SetDestination(walkPoint);
        }

        Vector3 distanceToWalkPoint = transform.position - walkPoint;

        //Walkpoint reached
        if(distanceToWalkPoint.magnitude < 1f){
            walkPointSet = false;
        }
    }

    private void SearchWalkPoint(){
        //calculate random point in range
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);
        animator.SetBool("Eat", false);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if(Physics.Raycast(walkPoint, -transform.up, 2f, whatIsGround)){
            walkPointSet = true;
        }
    }

    private void ChasePlayer(){
        animator.SetBool("Eat", false);
        agent.SetDestination(player.position);
    }

    private void AttackPlayer(){
        //make sure the enemy doesnt move while attacking
        agent.SetDestination(transform.position);

        transform.LookAt(player);

        if(!alreadyAttacked){
            //attack code here
            // Rigidbody rb = Instantiate(projectile,transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            // rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            // rb.AddForce(transform.up * 8f, ForceMode.Impulse);
            //
<<<<<<< HEAD
            alreadyAttacked = true;
            animator.SetBool("Eat", true);
=======
            StartCoroutine(waiter());
>>>>>>> 55069adfc5b60fb2e8800f284d1b656850938b0f
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack(){
        alreadyAttacked = false;
    }

    public void TakeDamage(int damage){
        health -= damage;

        if(health <= 0){
            Invoke(nameof(DestroyEnemy), .5f);
        }
    }

    private void DestroyEnemy(){
        Destroy(gameObject);
    }

    private void OnDrawGizmosSelected() {

        //Optional function that visualizes the attack and sight range of enemies
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);    
    }
<<<<<<< HEAD
=======

    IEnumerator waiter()
    {
            alreadyAttacked = true;
            animator.SetBool("Eat", true);

            audioSource.PlayOneShot(chickenSound);
            yield return new WaitForSeconds(1);
    }
>>>>>>> 55069adfc5b60fb2e8800f284d1b656850938b0f
}
