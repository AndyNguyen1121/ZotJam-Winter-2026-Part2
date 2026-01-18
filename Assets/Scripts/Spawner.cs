using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    // The list of enemy prefabs to spawn
    public List<GameObject> leftEnemies;
    public List<GameObject> middleEnemies;
    public List<GameObject> rightEnemies;
    public List<GameObject> spawnPositions;

    public int lastSpawn = 300;

    // time until next spawn
    public float spawnRate = 2f;

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
        int randomList = Random.Range(0, 3);

        if (randomList == 0)
        {
            int randomIndex = Random.Range(0, leftEnemies.Count);
            GameObject enemyPrefab = leftEnemies[randomIndex];

            // Picks a random spawn point (one of the three lanes)
            int randomSpawn = Random.Range(0, spawnPositions.Count);
            GameObject spawnPoint = spawnPositions[0];

            Vector3 spawn = spawnPoint.transform.position;
            spawn.z += Random.Range(600, 1500); ;

            // Instantiates enemy car
            GameObject enemyObject = Instantiate(enemyPrefab, spawn, spawnPoint.transform.rotation);
            enemyObject.AddComponent<EnemyCar>();
        }
        else if (randomList == 1)
        {
            int randomIndex = Random.Range(0, middleEnemies.Count);
            GameObject enemyPrefab = middleEnemies[randomIndex];

            // Picks a random spawn point (one of the three lanes)
            int randomSpawn = Random.Range(0, spawnPositions.Count);
            GameObject spawnPoint = spawnPositions[1];

            Vector3 spawn = spawnPoint.transform.position;
            spawn.z += Random.Range(600, 1500);

            // Instantiates enemy car
            GameObject enemyObject = Instantiate(enemyPrefab, spawn, spawnPoint.transform.rotation);
            enemyObject.AddComponent<EnemyCar>();
        }
        else
        {
            int randomIndex = Random.Range(0, rightEnemies.Count);
            GameObject enemyPrefab = rightEnemies[randomIndex];

            // Picks a random spawn point (one of the three lanes)
            int randomSpawn = Random.Range(0, spawnPositions.Count);
            GameObject spawnPoint = spawnPositions[2];

            Vector3 spawn = spawnPoint.transform.position;
            spawn.z += Random.Range(600, 1500); ;

            // Instantiates enemy car
            GameObject enemyObject = Instantiate(enemyPrefab, spawn, spawnPoint.transform.rotation);
            enemyObject.AddComponent<EnemyCar>();
        }
    }
}