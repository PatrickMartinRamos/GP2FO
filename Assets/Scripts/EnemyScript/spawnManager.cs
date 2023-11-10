using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class spawnManager : MonoBehaviour
{
    public GameObject enemy;
    public Transform[] spawnPoints;

    public float spawnInterval;
    private Coroutine spawnCoroutine;
    gameManager gameManager;
    public void Start()
    {
        gameManager = FindAnyObjectByType<gameManager>();
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (gameManager.shouldSpawn)
        {
            int randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Instantiate(enemy, spawnPoints[randomSpawnPoint].position, Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
    public void startSpawningEnemy()
    {
        gameManager.shouldSpawn = true;
        spawnCoroutine = StartCoroutine(SpawnEnemies());
    }
}
