using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] string levelSelect;
    [SerializeField] string mainMenu;
    [SerializeField] GameObject pauseScreen;

    LevelManager levelManager;
    Player player;
    // Start is called before the first frame update
    void Start()
    {
        levelManager = FindObjectOfType<LevelManager>();
        player = FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Time.timeScale == 0f)
            {
                ResumeGame();
            }
            else
            {
                ActivePauseMenu();
            }
        }
    }

    private void ActivePauseMenu()
    {
        Time.timeScale = 0;
        pauseScreen.SetActive(true);
        player.isAlive = false;
        levelManager.levelMusic.Pause();
    }

    public void ResumeGame()
    {
        pauseScreen.SetActive(false);
        Time.timeScale = 1f;
        player.isAlive = true;
        levelManager.levelMusic.Play();
    }

    public void LevelSelect()
    {
        PlayerPrefs.SetInt("PlayerLives", levelManager.currentLives);
        PlayerPrefs.SetInt("CoinCount", levelManager.coinCount);
        Time.timeScale = 1f;
        SceneManager.LoadScene(levelSelect);
    }

    public void QuitToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(mainMenu);
    }
}
