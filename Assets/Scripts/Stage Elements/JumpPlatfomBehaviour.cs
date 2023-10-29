using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatfomBehaviour : MonoBehaviour
{
    private PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        if (other == GameObject.FindGameObjectWithTag("Player"))
        {
            player.superJump();
        }
    }
}
