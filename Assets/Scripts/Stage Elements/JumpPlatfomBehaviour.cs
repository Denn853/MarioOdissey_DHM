using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatfomBehaviour : MonoBehaviour
{
    private CharacterController controller;
    private bool isJumping = false;
    private float jumpForce = 0.3f;

    private void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    private void Update()
    {
        if (controller.isGrounded)
        {
            if (isJumping)
            {
                Vector3 jumpVector = Vector3.up * jumpForce;
                controller.Move(jumpVector);
            }
        }

    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.CompareTag("JumpPlatform") && !isJumping)
        {
            Debug.Log(hit.gameObject.tag);
            // Detectamos la colisión con la plataforma de rebote y aplicamos el impulso hacia arriba
            isJumping = true;
        }
    }
}
