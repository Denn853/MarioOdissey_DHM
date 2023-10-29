using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPlatfomBehaviour : MonoBehaviour
{
    [SerializeField] private PlayerMovement player;

    private void OnTriggerEnter(Collider other)
    {
        player.superJump();
    }
}
