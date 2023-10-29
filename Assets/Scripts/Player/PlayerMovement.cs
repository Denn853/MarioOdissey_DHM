using System.Collections;
using System.Collections.Generic;
using System.Runtime.ConstrainedExecution;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.EnhancedTouch;

public class PlayerMovement : MonoBehaviour
{
    [Header("Camera")]
    [SerializeField] private Camera camera;


    private CharacterController controller;
    private Input_Manager inputManager;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 followDirecction = Vector3.zero;


    [Header("Movement")]
    [SerializeField] private float maxVelocity = 8f; // max velocityXZ
    private float velocity = 0f; // initial velocityXZ
    [SerializeField] private float acceleration = 5f;
    [SerializeField] private float decceleration = 5f;


    [Header("Rotation")]
    [SerializeField] private float rotationSpeed = 1f;


    [Header("Jump")]
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float jumpForce = 8f;

    private int maxJumps = 2; // 3 jumps
    private float jumpIncrement = 5f;
    private int jumpCounter = 0;
    public float jumpTimer = 0f;


    private bool isInJumpPlatform = false;
    private float jumpPlatformForce = 0.05f;


    [Header("Crouch")]
    [SerializeField] private bool canCrouch;



    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        inputManager = Input_Manager._INPUT_MANAGER;

        //Bloquea cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        Movement();
        TripleJump();
        Crouch();
    }


    private void Movement()
    {
        float horizontalInput = inputManager.GetLeftAxisValue().x;
        float verticalInput = inputManager.GetLeftAxisValue().y;

        // Calcular dirección XZ (movimiento)
        Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(horizontalInput, 0f, verticalInput);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            //rotation
            Quaternion rotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.5f * Time.deltaTime);
            gameObject.transform.forward = direction;


            // acceleration
            velocity += acceleration * Time.deltaTime;
            followDirecction = direction;
        }
        else
        {
            // decceleration
            velocity -= decceleration * Time.deltaTime;
            direction.x = followDirecction.x;
            direction.z = followDirecction.z;
        }

        velocity = Mathf.Clamp(velocity, 0f, maxVelocity);

        // Calcular velocidad XZ
        finalVelocity.x = direction.x * velocity; // XZ
        finalVelocity.z = direction.z * velocity; // XZ

        // Asignar dirección Y
        direction.y = -1f;

        // Aplica gravedad dependiendo si esta en el suelo o no
        finalVelocity.y += direction.y * gravity * Time.deltaTime;


        if (isInJumpPlatform)
        {
            Vector3 jumpVector = Vector3.up * jumpPlatformForce;
            controller.Move(jumpVector);
        }
        else
        {
            controller.Move(finalVelocity * Time.deltaTime);
        }

        controller.Move(finalVelocity * Time.deltaTime);
    }


    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("JumpPlatform") && !isInJumpPlatform)
        {
            Debug.Log(hit.gameObject.tag);
            // Detectamos la colisión con la plataforma de rebote y aplicamos el impulso hacia arriba
            isInJumpPlatform = true;
        }
        else
        {
            isInJumpPlatform = false;
        }
    }


    private void TripleJump()
    {
        // Calcular gravedad 
        if (controller.isGrounded)
        {
            if (inputManager.GetJumpButtonPressed())
            {
                finalVelocity.y = jumpForce + jumpIncrement * jumpCounter;
                jumpTimer = 0.5f;

                if (jumpCounter < maxJumps)
                {
                    jumpCounter++;
                }
                else
                {
                    jumpCounter = 0;
                }

            }

            jumpTimer -= Time.deltaTime;

            if (jumpTimer <= 0f)
            {
                jumpCounter = 0;
            }
        }
    }


    private void Crouch()
    {
        if (inputManager.GetCrouchButtonPressed() && canCrouch == false)
        {
            canCrouch = true;
        }
        else
        {
            canCrouch = false;
        }

        if (canCrouch == true)
        {
            finalVelocity *= 0.5f;
            controller.height = 1f;
        }
        else
        {
            controller.height = 2f;
        }
    }
}