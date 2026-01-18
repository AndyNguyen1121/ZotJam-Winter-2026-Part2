using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.LegacyInputHelpers;

public class Car : MonoBehaviour
{
    public static Car Instance;

    public Camera playerCamera;
    public float moveSpeed = 1f;
    public float slerpSpeed = 10f;


    private Vector3 moveDirection = Vector3.zero;
    private Rigidbody rb;
    private bool isMoving = false;
    private bool movingRight;
    private bool movingLeft;

    [Header("Rotation")]
    public GameObject carBody;
    public GameObject carParent;
    public float minYRotation = -45f;
    public float maxYRotation = 45f;
    public float minZRotation = -100f;
    public float maxZRotation = -80f;
    private Vector3 localVelocity;

    [SerializeField] private float rotationSpeed;
    [SerializeField] private float rotationYMultiplier;
    [SerializeField] private float rotationZMultiplier;

    [Header("InputTimer")]
    public float timeSinceSwitchedInput;
    public float timeDelayThreshold = 0.5f;
    public float rotationLerpSpeed = 3f;

    [Header("Wheel Rotation")]
    public GameObject wheel1;
    public GameObject wheel2;
    private Tween spinTween1;
    private Tween spinTween2;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Debug.Log("More than one player car in scene");
    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        spinTween1 = wheel1.transform.DOLocalRotate(
                new Vector3(360, 0, 0),
                0.01f,
                RotateMode.FastBeyond360
                )
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);

        spinTween2 = wheel2.transform.DOLocalRotate(
                new Vector3(360, 0, 0),
                0.01f,
                RotateMode.FastBeyond360
                )
                .SetRelative(true)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Restart);

        LevelManager.Instance.levelMoveDirection = -transform.forward;
    }

    void Update()
    { 
        Vector3 right = transform.right;

        // Left movement
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            if (movingRight)
            {
                timeSinceSwitchedInput = 0;
            }

            isMoving = true;
            moveDirection = -right * moveSpeed;

            movingLeft = true;
            movingRight = false;

            Debug.Log("daa");
        }
        // Right movement
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (movingLeft)
            {
                timeSinceSwitchedInput = 0;
            }

            isMoving = true;
            moveDirection = right * moveSpeed;

            movingLeft = false;
            movingRight = true;
        }
        else
        {
            isMoving = false;
            movingLeft = false;
            movingRight = false;
            moveDirection = Vector3.zero;
        }

        rb.linearVelocity = moveDirection;
        HandleInputTimers();
        if (timeSinceSwitchedInput > timeDelayThreshold)
        {
            rotationSpeed = Mathf.Lerp(rotationSpeed, 20f, rotationLerpSpeed * Time.deltaTime);
        }
        else
        {
            rotationSpeed = Mathf.Lerp(rotationSpeed, 6f, 20f * Time.deltaTime);
        }
        HandleRotation();

    }

    void HandleRotation()
    {
        localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        localVelocity.x = Mathf.Clamp(localVelocity.x, minYRotation, maxYRotation);
        Quaternion targetRotation = Quaternion.Euler(transform.localRotation.eulerAngles.x, localVelocity.x * rotationYMultiplier, transform.localRotation.eulerAngles.z);
        transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);

        float speedFraction = Mathf.Abs(localVelocity.x) / maxYRotation;
        float targetCarBodyZRotation = maxZRotation * speedFraction * Mathf.Sign(localVelocity.x);
        Quaternion targetCarBodyRotation = Quaternion.Euler(carBody.transform.localRotation.eulerAngles.x, carBody.transform.localRotation.eulerAngles.y, targetCarBodyZRotation * rotationZMultiplier);
        carBody.transform.localRotation = Quaternion.Slerp(carBody.transform.localRotation, targetCarBodyRotation, rotationSpeed * Time.deltaTime);

    }

    void HandleInputTimers()
    {
        timeSinceSwitchedInput += Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RotationCollider"))
        {
            Quaternion targetRotation = Quaternion.Euler(0, carParent.transform.eulerAngles.y + 90, 0);
            carParent.transform.transform.rotation = targetRotation;
            LevelManager.Instance.levelMoveDirection = -transform.forward;
        }

        Debug.Log("hit trigger");
    }
}
