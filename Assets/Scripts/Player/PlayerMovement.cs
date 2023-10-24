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

    private Vector3 finalVelocity = Vector3.zero;

    [SerializeField] private float velocityXZ = 5f;

    [SerializeField] private float gravity = 20f;
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float coyoteTime = 1f;

    [SerializeField] private float maxJumps = 3f;
    [SerializeField] private float jumpsDelay = 3f;
    
    [SerializeField] private float rotationSpeed = 1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();

        //Bloquea cursor
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");


        // Calcular dirección XZ (movimiento)
        Vector3 direction = Quaternion.Euler(0f, camera.transform.eulerAngles.y, 0f) * new Vector3(horizontalInput, 0f, verticalInput);
        direction.Normalize();

       
        // Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;


        controller.Move(finalVelocity * Time.deltaTime);

        if (direction != Vector3.zero)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(direction, Vector3.up);

            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, 0.5f * Time.deltaTime);
            gameObject.transform.forward = direction;
        }


        // Asignar dirección Y
        direction.y = -1f;

        // Calcular gravedad 
        if (controller.isGrounded)
        {
            // El contador va bajando hasta que llega a 0
            jumpsDelay -= Time.deltaTime;

            // Primer Salto
            //if (Input_Manager._INPUT_MANAGER.GetSouthButtonPressed() && maxJumps == 3)
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
                //jumpsDelay = 5f;
            }

        }
        else
        {
            finalVelocity.y += direction.y * gravity * Time.deltaTime;
            coyoteTime -= Time.deltaTime;

            if (Input.GetKey(KeyCode.Space) && coyoteTime >= 0f)
            {
                finalVelocity.y = jumpForce;
                coyoteTime = 0f;
            }
        }
    }
}