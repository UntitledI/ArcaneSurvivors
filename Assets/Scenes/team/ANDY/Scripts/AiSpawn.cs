using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawn : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private float spawnDistance = 50f;
    [SerializeField] private int totalBoars = 100;
    [SerializeField] private int boarsPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 2f;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        int waves = totalBoars / boarsPerWave;
        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < boarsPerWave; j++)
            {
                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.y = 0f;
                Vector3 spawnPosition = playerTransform.position + randomDirection * spawnDistance;

                GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                MonsterMoving monsterMoving = monster.GetComponent<MonsterMoving>();
                if (monsterMoving != null)
                {
                    monster.transform.LookAt(playerTransform);
                }
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}