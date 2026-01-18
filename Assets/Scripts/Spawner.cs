using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // The list of enemy prefabs to spawn
    public List<GameObject> enemies;
    public List<GameObject> spawnPositions;

    // time until next spawn
    public float spawnRate = 1.0f;

    void Start()
    {
        // Infinite spawning loop
        StartCoroutine(SpawnRoutine());
    }

    IEnumerator SpawnRoutine()
    {
        while (true){
            yield return new WaitForSeconds(spawnRate);
            SpawnEnemy();
        }
    }

    void SpawnEnemy()
    {
        // Picks a random enemy from the list
        int randomIndex = Random.Range(0, enemies.Count);
        GameObject enemyPrefab = enemies[randomIndex];

        // Picks a random spawn point (one of the three lanes)
        int randomSpawn = Random.Range(0, spawnPositions.Count);
        GameObject spawnPoint = spawnPositions[randomSpawn];

        // Instantiates enemy car
        GameObject enemyObject = Instantiate(enemyPrefab, spawnPoint.transform.position, spawnPoint.transform.rotation);
        enemyObject.AddComponent<EnemyCar>();
    }
}