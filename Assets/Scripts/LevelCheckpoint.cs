using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelCheckpoint : MonoBehaviour
{
    [SerializeField] string levelToLoad;
    [SerializeField] GameObject fire;
    [SerializeField] bool unlocked;
    // Start is called before the first frame update
    void Start()
    {
        PlayerPrefs.SetInt("Level 1 Cinematic", 1);
        if (PlayerPrefs.GetInt(levelToLoad) == 1)
        {
            unlocked = true;
        }
        else
        {
            unlocked = false;
        }

        if (unlocked)
        {
            fire.SetActive(true);
        }
        else
        {
            fire.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (Input.GetButtonDown("Jump"))
            {
                SceneManager.LoadScene(levelToLoad);
            }
        }
    }
}
