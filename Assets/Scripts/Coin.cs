using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    // State
    [SerializeField] int coinValue;
    [SerializeField] AudioClip coinPickUpSFX;
    // Cached component references
    LevelManager levelManager;

    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(coinPickUpSFX, Camera.main.transform.position);
            levelManager.AddCoins(coinValue);
            Destroy(gameObject);
        }
    }
}
