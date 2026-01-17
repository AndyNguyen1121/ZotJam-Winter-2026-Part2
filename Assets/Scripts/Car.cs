using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.XR.LegacyInputHelpers;

public class Car : MonoBehaviour
{
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
            moveDirection.x = -right.x * moveSpeed;

            movingLeft = true;
            movingRight = false;
        }
        // Right movement
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            if (movingLeft)
            {
                timeSinceSwitchedInput = 0;
            }

            isMoving = true;
            moveDirection.x = right.x * moveSpeed;

            movingLeft = false;
            movingRight = true;
        }
        else
        {
            isMoving = false;
            movingLeft = false;
            movingRight = false;
            moveDirection.x = 0;
        }

        rb.linearVelocity = moveDirection;
        HandleInputTimers();
        if (timeSinceSwitchedInput > timeDelayThreshold)
        {
            rotationSpeed = Mathf.Lerp(rotationSpeed, 10f, rotationLerpSpeed * Time.deltaTime);
        }
        else
        {
            rotationSpeed = Mathf.Lerp(rotationSpeed, 3f, 20f * Time.deltaTime);
        }
        HandleRotation();

    }

    void HandleRotation()
    {
        localVelocity = transform.InverseTransformDirection(rb.linearVelocity);
        localVelocity.x = Mathf.Clamp(localVelocity.x, minYRotation, maxYRotation);
        Quaternion targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, localVelocity.x * rotationYMultiplier, transform.rotation.eulerAngles.z);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        float speedFraction = Mathf.Abs(localVelocity.x) / maxYRotation;
        float targetCarBodyZRotation = maxZRotation * speedFraction * Mathf.Sign(localVelocity.x);
        Quaternion targetCarBodyRotation = Quaternion.Euler(carBody.transform.eulerAngles.x, carBody.transform.eulerAngles.y, targetCarBodyZRotation * rotationZMultiplier);
        carBody.transform.rotation = Quaternion.Slerp(carBody.transform.rotation, targetCarBodyRotation, rotationSpeed * Time.deltaTime);

    }

    void HandleInputTimers()
    {
        timeSinceSwitchedInput += Time.deltaTime;
    }
}
