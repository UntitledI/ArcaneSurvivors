using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicAiSpawn : MonoBehaviour
{
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private float spawnDistance = 50f;
    [SerializeField] private int totalMonsters = 100;
    [SerializeField] private int monstersPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 2f;

    private Transform playerTransform;
    private List<Transform> spawnPoints = new List<Transform>();

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        // Find all spawn points and add them to the list
        GameObject[] spawnPointObjects = GameObject.FindGameObjectsWithTag("SpawnPoint");
        foreach (GameObject spawnPointObject in spawnPointObjects)
        {
            spawnPoints.Add(spawnPointObject.transform);
        }

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

                // Choose a random spawn point from the list
                Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];

                // Calculate the spawn position based on the random spawn point and distance
                Vector3 spawnPosition = randomSpawnPoint.position + randomDirection * spawnDistance;

                GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}