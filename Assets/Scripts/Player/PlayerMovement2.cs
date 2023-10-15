using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement2 : MonoBehaviour
{
    public float speed;
    private CharacterController controller;
    private Vector3 moveDirection = Vector3.zero;
    [SerializeField]
    private float rotationSpeed = 1f;

    private void Start()
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

        // Rotar el personaje hacia la dirección de movimiento
        if (movementDirection != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movementDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, toRotation, Time.deltaTime * rotationSpeed);
        }

        // Mover el personaje
        moveDirection = movementDirection * speed;
        controller.Move(moveDirection * Time.deltaTime);
    }
}
