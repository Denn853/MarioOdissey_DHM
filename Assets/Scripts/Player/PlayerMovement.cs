using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private Camera camera;

    private CharacterController controller;
    private Input_Manager inputManager;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 followDirecction = Vector3.zero;

    [SerializeField] private float velocity = 0f; // velocityXZ
    [SerializeField] private float maxVelocity = 5f; // velocityXZ
    [SerializeField] private float acceleration = 5f; 
    [SerializeField] private float decceleration = 5f; 

    [SerializeField] private float gravity = 20f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float coyoteTime = 1f;

    [SerializeField] private int maxJumps = 2; // 3 jumps
    [SerializeField] private float jumpIncrement = 5f;
    [SerializeField] private int jumpCounter = 0;
    
    [SerializeField] private float rotationSpeed = 1f;

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
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
            gameObject.transform.forward = direction;

            // acceleration
            velocity += acceleration * Time.deltaTime;
            followDirecction = direction;
        }
        else
        {
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
        if (controller.isGrounded)
        {
            finalVelocity.y = direction.y * gravity * Time.deltaTime;
        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
        }
        Debug.Log(finalVelocity.y);
        controller.Move(finalVelocity * Time.deltaTime);
    }

    private void TripleJump()
    {
        // Calcular gravedad 
        if (controller.isGrounded)
        {
            if (inputManager.GetJumpButtonPressed())
            {
                Debug.Log("jumpiiiiing");

                finalVelocity.y = jumpForce + jumpIncrement * jumpCounter;

                if (jumpCounter < maxJumps)
                {
                    jumpCounter++;
                }
                else
                {
                    jumpCounter = 0;
                }
            }

            // El contador va bajando hasta que llega a 0
            //jumpsDelay -= Time.deltaTime;
            /*
            // Primer Salto
            //if (Input_Manager._INPUT_MANAGER.GetJumpButtonPressed() && maxJumps == 3)
            if (Input.GetKey(KeyCode.Space) && maxJumps == 3)
            {
                finalVelocity.y = jumpForce;
                maxJumps--;
                jumpsDelay = 3f;

            }

            // Segundo Salto
            else if (Input.GetKey(KeyCode.Space) && maxJumps == 2)
            {
                finalVelocity.y = jumpForce + 5;
                maxJumps--;
                jumpsDelay = 3f;

            }

            // Tercer Salto.
            else if (Input.GetKey(KeyCode.Space) && maxJumps == 1)
            {
                finalVelocity.y = jumpForce + 10;
                maxJumps--;
                jumpsDelay = 3f;

            }

            else if (maxJumps == 0 && jumpsDelay < 3f)
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                coyoteTime = 1f;
                maxJumps = 3;
                jumpsDelay = 3f;
            }
            */

            /*
            // Resetear el contador cuando llega a 0
            if (jumpsDelay <= 0f || maxJumps == 0)
            {
                jumpsDelay = 3f;
                maxJumps = 3; // Reiniciar el número de saltos disponibles
            }

            // Primer Salto
            if (inputManager.GetJumpButtonPressed() && maxJumps > 0)
            {
                finalVelocity.y = jumpForce + (3 - maxJumps) * 5;
                maxJumps--;
                jumpsDelay = 3f;
            }

            else if (maxJumps == 0 && jumpsDelay < 3f)
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                coyoteTime = 1f;
            }

        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            coyoteTime -= Time.deltaTime;

            if (inputManager.GetJumpButtonPressed() && coyoteTime >= 0f)
            {
                finalVelocity.y = jumpForce;
                coyoteTime = 0f;
            }*/
        }
    }

    private void Couch()
    {

    }
}