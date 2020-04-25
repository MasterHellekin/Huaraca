using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraLife : MonoBehaviour
{
    // Config
    [SerializeField] int livesToGive;
    [SerializeField] AudioClip healthPickUpSFX;

    private LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            AudioSource.PlayClipAtPoint(healthPickUpSFX, Camera.main.transform.position);
            levelManager.AddLives(livesToGive);
            Destroy(gameObject);
        }
    }
}
