using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;

    public bool shouldSpawn = false;
    public float spawnInterval;
    private Coroutine spawnCoroutine;

    public void Start()
    {
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (shouldSpawn)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[randomSpawnPoint].position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void startSpawningEnemy()
    {
        shouldSpawn = true;
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }
}
