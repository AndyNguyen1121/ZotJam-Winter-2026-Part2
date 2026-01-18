using DG.Tweening;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 lastPosition;
    [SerializeField] private float minRotation = -45f;
    [SerializeField] private float maxRotation = 45f;
    private Vector3 localVelocity;
    [SerializeField] private GameObject carModel;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationMultiplier;

    private Vector3 randomMinPoint;
    private Vector3 randomMaxPoint;
    [SerializeField] private Transform minPosition;
    [SerializeField] private Transform maxPosition;
    [SerializeField] private float speed = 1f;

    public bool sideToSide = true;

    [Header("Particles")]
    [SerializeField] private GameObject explosionParticle;

    private void Start()
    {
        
        
        lastPosition = transform.localPosition;

        if (minPosition != null && maxPosition != null)
        {
            randomMinPoint = Vector3.Lerp(minPosition.localPosition, maxPosition.localPosition, Random.Range(0, 0.3f));
            randomMaxPoint = Vector3.Lerp(minPosition.localPosition, maxPosition.localPosition, Random.Range(0.7f, 1f));
        }
        

        if (sideToSide)
        {
            transform.DOLocalMove(randomMaxPoint, speed).From(randomMinPoint).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutSine, 2);
        }

        // Destroys after 20 seconds of creation
        Destroy(gameObject, 20f);
    }

    private void Update()
    {
        velocity = (transform.localPosition - lastPosition) / Time.deltaTime;
        lastPosition = transform.localPosition;

        localVelocity = transform.InverseTransformDirection(velocity);
        localVelocity.x = Mathf.Clamp(localVelocity.x, minRotation, maxRotation);
        Quaternion targetRotation = Quaternion.Euler(carModel.transform.rotation.eulerAngles.x, localVelocity.x * rotationMultiplier, carModel.transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        
    }

    void FixedUpdate()
    {
        transform.Translate(LevelManager.Instance.levelMoveDirection * LevelManager.Instance.enemyMoveSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Debug.Log("Enemy Car Collided with Wall");
            Instantiate(explosionParticle, transform.position, Quaternion.identity);
            // The tween must be stopped BEFORE or AS the object is destroyed
            transform.DOKill();
            Destroy(gameObject);
        }
    }

    // COMPLETE FIX: Add this method to handle the Destroy(gameObject, 20f) timer
    // and any other destruction cases
    private void OnDestroy()
    {
        transform.DOKill();
    }
}
