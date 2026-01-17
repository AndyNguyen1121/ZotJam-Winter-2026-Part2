using DG.Tweening;
using UnityEngine;

public class SimpleRoadManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        transform.DOMoveZ(transform.position.z - 500f , 10f).SetEase(Ease.Linear);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
