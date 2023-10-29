using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private static int coinsCollected;
    
    [SerializeField] private AudioClip pickCoinSound;

    private Audio_Manager audioManager;

    private void Start()
    {
        coinsCollected = 0;
        audioManager = Audio_Manager._AUDIO_MANAGER;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("Player"))
        {
            coinsCollected++;
            audioManager.RunSound(pickCoinSound);
            Destroy(gameObject);
        }
    }
}
