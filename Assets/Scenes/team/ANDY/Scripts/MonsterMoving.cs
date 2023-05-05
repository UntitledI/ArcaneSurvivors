using UnityEngine;
using System.Collections;
using UnityEngine.AI;

public class MonsterMoving : MonoBehaviour {
    private GameObject player;
    private NavMeshAgent navMeshAgent;

    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    void Update () {
        navMeshAgent.SetDestination(player.transform.position);
    }
}