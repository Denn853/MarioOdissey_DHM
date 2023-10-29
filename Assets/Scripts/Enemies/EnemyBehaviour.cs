using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour
{
    private Audio_Manager audioManager;
    [SerializeField] private AudioClip dieSound;

    [SerializeField] private GameObject player;

    [SerializeField] private loseUI UI;

    private void Start()
    {
        audioManager = Audio_Manager._AUDIO_MANAGER;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player)
        {
            Destroy(player);

            audioManager.RunSound(dieSound);
            //UI.ShowLoseUI();

            Debug.Log("GAME OVER - YOU LOSE D:");
        }
    }
}
