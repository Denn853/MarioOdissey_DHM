using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    private CharacterController controller;

    private Vector3 finalVelocity = Vector3.zero;
    private Vector3 finalDirection = Vector3.zero;

    [SerializeField] 
    private float velocityXZ = 5f;

    [SerializeField]
    private float gravity = 20f;

    [SerializeField]
    private float jumpForce = 8f;
    
    [SerializeField]
    private float coyoteTime = 1f;
    
    [SerializeField]
    private float rotationSpeed = 1f;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calcular la dirección de movimiento
        Vector3 movementDirection = new Vector3(horizontalInput, 0, verticalInput);
        movementDirection.Normalize();

        // Calcular dirección XZ
        Vector3 direction = Input.GetAxis("Vertical") * transform.forward + Input.GetAxis("Horizontal") * transform.right;
        direction.Normalize();

        // Rotar el personaje hacia la dirección de movimiento
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }

        // Calcular velocidad XZ
        finalVelocity.x = direction.x * velocityXZ;
        finalVelocity.z = direction.z * velocityXZ;

        // Asignar dirección Y
        direction.y = -1f;

        // Calcular gravedad
        if (controller.isGrounded)
        {
            if (Input.GetKey(KeyCode.Space))
            {
                finalVelocity.y = jumpForce;
            }
            else
            {
                finalVelocity.y = direction.y * gravity * Time.deltaTime;
                coyoteTime = 1f;
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

        // Mover el personaje
        controller.Move(finalVelocity * Time.deltaTime);

        // Hacer que el personaje mire hacia donde camina
        finalDirection = movementDirection * rotationSpeed;
    }
}