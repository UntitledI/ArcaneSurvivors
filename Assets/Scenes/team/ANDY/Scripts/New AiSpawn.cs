using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewAiSpawn : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private int totalMonsters = 100;
    [SerializeField] private int monstersPerWave = 5;
    [SerializeField] private float timeBetweenWaves = 2f;
    private List<Transform> spawnPoints = new List<Transform>();

    void Start()
    {
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
            List<Transform> chosenSpawnPoints = new List<Transform>();
            for (int j = 0; j < 4; j++) // Choose 4 random spawn points for this wave
            {
                Transform chosenSpawnPoint;
                do
                {
                    chosenSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Count)];
                } while (chosenSpawnPoints.Contains(chosenSpawnPoint)); // Ensure no duplicate spawn points are chosen
                chosenSpawnPoints.Add(chosenSpawnPoint);
            }

            foreach (Transform spawnPoint in chosenSpawnPoints)
            {
                for (int k = 0; k < monstersPerWave; k++)
                {
                    Vector3 spawnPosition = spawnPoint.position;

                    GameObject monster = Instantiate(monsterPrefab, spawnPosition, Quaternion.identity);
                }
            }

            yield return new WaitForSeconds(timeBetweenWaves);
        }
    }
}