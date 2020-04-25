using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    // Config
    [SerializeField] bool checkpointActive = false;
    [SerializeField] GameObject fire;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            checkpointActive = true;
            fire.SetActive(true);
        }
    }
}
