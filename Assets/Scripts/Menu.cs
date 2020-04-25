using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Menu : MonoBehaviour
{
    public void StartFirstLevel()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void StartCredits()
    {
        SceneManager.LoadScene("Credits");
    }

    public void StartMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void StartControls()
    {
        SceneManager.LoadScene("Controls");
    }

    public void StartHistory()
    {
        SceneManager.LoadScene("History");
    }
}
