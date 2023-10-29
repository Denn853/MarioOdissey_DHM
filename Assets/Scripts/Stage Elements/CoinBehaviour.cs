using System.Collections;
using System.Collections.Generic;
using System.Security;
using UnityEngine;

public class CoinBehaviour : MonoBehaviour
{
    private static int coinsCollected;
    
    private Audio_Manager audioManager;
    [SerializeField] private AudioClip pickCoinSound;
    [SerializeField] private AudioClip winSound;
    [SerializeField] private winUI UI;

    [SerializeField] private float velocity = 2.0f;
    [SerializeField] private float maxDistance = 1.0f;

    private Vector3 startPosition;

    private void Start()
    {
        coinsCollected = 0;
        audioManager = Audio_Manager._AUDIO_MANAGER;

        startPosition = transform.position;
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

    private void Update()
    {
        // ANIMATION
        transform.rotation *= Quaternion.Euler(0, 0.5f, 0);

        float yMovement = maxDistance * Mathf.Sin(velocity * Time.time);
        transform.position = startPosition + new Vector3(0, yMovement, 0);
        
        Debug.Log(coinsCollected);

        if (coinsCollected == 4)
        {
            Debug.Log(coinsCollected);
            //audioManager.RunSound(winSound);
            //UI.ShowWinUI();

            Debug.Log("GAME OVER - YOU WIN :D");
        }
    }
}
