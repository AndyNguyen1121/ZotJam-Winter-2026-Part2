using DG.Tweening;
using UnityEngine;

public class SimpleRoadManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        transform.Translate(LevelManager.Instance.levelMoveDirection * LevelManager.Instance.levelMoveSpeed * Time.deltaTime);
    }
}
