using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(CharacterController))]
public class Car : MonoBehaviour
{
    public Camera playerCamera;
    public float moveSpeed = 1f;
    public float rotateSpeed = 0.5f;
    public float rotationLimit = 0.3f;
    public float moveLimit = 2f;


    private Vector3 moveDirection = Vector3.zero;
    private CharacterController characterController;
    private bool isMoving = false;


    void Start()
    {
        characterController = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }


    void Update()
    {
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Left movement
        if(Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow)){
            isMoving = true;
            if (transform.position.x > -moveLimit){
                moveDirection.x = -right.x * moveSpeed;
            }
            else{
                moveDirection.x = 0;
            }
           
            // Rotates the car towards the left
            if (transform.rotation.y > -rotationLimit){
                transform.rotation *= Quaternion.Euler(0,  -rotateSpeed, 0);
            }
        }
        // Right movement
        else if(Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow)){
            isMoving = true;
            if (transform.position.x < moveLimit){
                moveDirection.x = right.x * moveSpeed;
            }
            else{
                moveDirection.x = 0;
            }
            // Rotates the car towards the right
            if (transform.rotation.y < rotationLimit){
                transform.rotation *= Quaternion.Euler(0,  rotateSpeed, 0);
            }
        }
        else{
            isMoving = false;
            moveDirection.x = 0;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
   
        characterController.Move(moveDirection * Time.deltaTime);
    }
}
