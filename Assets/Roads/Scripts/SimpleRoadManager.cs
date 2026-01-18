using DG.Tweening;
using UnityEngine;

public class SimpleRoadManager : MonoBehaviour
{

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.Translate(LevelManager.Instance.levelMoveDirection * LevelManager.Instance.levelMoveSpeed * Time.deltaTime);
    }
}
