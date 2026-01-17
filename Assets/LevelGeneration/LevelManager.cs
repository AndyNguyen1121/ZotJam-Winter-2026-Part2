using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    public Vector3 levelMoveDirection;
    public float levelMoveSpeed = 10f;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
        {
            Debug.Log("More than one level manager in the scene");
        }

        
    }
}
