using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AiSpawnRecreate : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private float spawnDistance = 50f;
    [SerializeField] private int totalMonsters = 100;
    [SerializeField] private int monstersPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 2f;

    private Transform playerTransform;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        StartCoroutine(SpawnWaves());
    }

    private IEnumerator SpawnWaves()
    {
        int waves = totalMonsters / monstersPerWave;
        for (int i = 0; i < waves; i++)
        {
            for (int j = 0; j < monstersPerWave; j++)
            {
                Vector3 randomDirection = Random.onUnitSphere;
                randomDirection.y = 0f;
                Vector3 spawnPosition = playerTransform.position + randomDirection * spawnDistance;

                GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}