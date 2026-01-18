using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Vector3 levelMoveDirection;
    public float levelMoveSpeed = 10f;
    public Transform playerTransform;
    public Transform spawnLocation;
    public GameObject testRoad;

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
        Instantiate(testRoad, spawnLocation.position, Quaternion.identity);
    }
}
