using DG.Tweening;
using UnityEngine;

public class SimpleRoadManager : MonoBehaviour
{

    public Transform endpoint;

    private void Start()
    {
        LevelManager.Instance.spawnLocation = endpoint;
    }
    void FixedUpdate()
    {
        transform.Translate(LevelManager.Instance.levelMoveDirection * LevelManager.Instance.levelMoveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            LevelManager.Instance.SpawnNewRoad();
            Destroy(gameObject, 10f);
        }
    }
}
