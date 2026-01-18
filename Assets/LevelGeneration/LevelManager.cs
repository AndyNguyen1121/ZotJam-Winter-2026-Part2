using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Vector3 levelMoveDirection;
    public float levelMoveSpeed = 10f;
    public Transform playerTransform;
    public Transform spawnLocation;
    

    public List<GameObject> roadPrefabs = new();

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("More than one level manager in the scene");
        }

        
    }

    private void Update()
    {
        levelMoveDirection = -playerTransform.forward;
    }

    public void SpawnNewRoad()
    {
        int randomIndex = Random.Range(0, (int)roadPrefabs.Count);
        Instantiate(roadPrefabs[randomIndex], spawnLocation.position, Quaternion.identity);
    }
}
