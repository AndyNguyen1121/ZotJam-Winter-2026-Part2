using UnityEngine;

public class EnemyCar : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start(){
        // Destroys after 10 seconds of creation
        Destroy(gameObject, 10f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void FixedUpdate()
    {
        transform.Translate(LevelManager.Instance.levelMoveDirection * LevelManager.Instance.levelMoveSpeed * Time.deltaTime);
    }
}
