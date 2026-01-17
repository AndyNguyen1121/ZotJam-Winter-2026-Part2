using DG.Tweening;
using UnityEditor.Rendering;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField] private Vector3 _velocity;
    [SerializeField] private Vector3 _lastPosition;
    [SerializeField] private float _minRotation = -45f;
    [SerializeField] private float _maxRotation = 45f;
    [SerializeField] private Vector3 _localVelocity;
    [SerializeField] private GameObject _carModel;
    [SerializeField] private float _rotationSpeed;
    [SerializeField] private float _rotationMultiplier;

    [SerializeField] private Vector3 randomMinPoint;
    [SerializeField] private Vector3 randomMaxPoint;
    [SerializeField] private Transform minPosition;
    [SerializeField] private Transform maxPosition;
    [SerializeField] private float speed = 1f;

    private void Start()
    {
        _lastPosition = transform.position;

        Vector3 randomMinPoint = Vector3.Lerp(minPosition.position, maxPosition.position, Random.Range(0, 0.3f));
        Vector3 randomMaxPoint = Vector3.Lerp(minPosition.position, maxPosition.position, Random.Range(0.7f, 1f));
        transform.DOMove(randomMaxPoint, speed).From(randomMinPoint).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.InOutCubic);
    }

    private void Update()
    {
        _velocity = (transform.position - _lastPosition) / Time.deltaTime;
        _lastPosition = transform.position;

        _localVelocity = transform.InverseTransformDirection(_velocity);
        _localVelocity.x = Mathf.Clamp(_localVelocity.x, _minRotation, _maxRotation);
        Quaternion targetRotation = Quaternion.Euler(_carModel.transform.rotation.eulerAngles.x, _velocity.x * _rotationMultiplier, _carModel.transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _rotationSpeed * Time.deltaTime);

        
    }
}
