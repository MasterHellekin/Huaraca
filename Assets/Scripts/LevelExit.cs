using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelExit : MonoBehaviour
{
    [SerializeField] float LevelLoadDelay = 2f;
    [SerializeField] float LevelExitSlowMoFactor = 0f;
    [SerializeField] AudioSource misionCompletedMusic;
    [SerializeField] string levelToUnlock;

    LevelManager levelManager;

    private void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(LoadNextLevel());
    }

    IEnumerator LoadNextLevel()
    {
        Time.timeScale = LevelExitSlowMoFactor;
        levelManager.levelMusic.Stop();
        misionCompletedMusic.Play();
        yield return new WaitForSecondsRealtime(LevelLoadDelay);
        Time.timeScale = 1f;
        PlayerPrefs.SetInt("coinCount", levelManager.coinCount);
        PlayerPrefs.SetInt("playerLives", levelManager.currentLives);
        PlayerPrefs.SetInt(levelToUnlock, 1);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }
}
