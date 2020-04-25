using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Dialogues : MonoBehaviour
{
    // Config
    [SerializeField] TextMeshProUGUI textDisplay;
    [SerializeField] TextAsset textFile;
    [SerializeField] string[] sentences;
    [SerializeField] Sprite[] sprites;
    [SerializeField] float typingSpeed;
    [SerializeField] GameObject continueButton;
    [SerializeField] GameObject nextLevelButton;
    [SerializeField] GameObject storyImage;

    AudioSource myAudioSource;

    // State
    private int index;

    private void Start()
    {
        myAudioSource = GetComponent<AudioSource>();
        StartCoroutine(Type());
    }

    private void Update()
    {
        if (textDisplay.text == sentences[index])
        {
            continueButton.SetActive(true);
        }
    }

    IEnumerator Type()
    {
        if (textFile != null)
        {
            sentences = textFile.text.Split('\n');
            foreach (char letter in sentences[index].ToCharArray())
            {
                textDisplay.text += letter;
                storyImage.GetComponent<Image>().sprite = sprites[index];
                myAudioSource.Play();
                myAudioSource.volume = 0.1f;
                yield return new WaitForSeconds(typingSpeed);
            }
        }
    }

    public void NextSentence()
    {
        continueButton.SetActive(false);
        if (index < sentences.Length - 1)
        {
            index++;
            textDisplay.text = "";
            StartCoroutine(Type());
        }
        else
        {
            textDisplay.text = "";
            continueButton.SetActive(false);
            nextLevelButton.SetActive(true);
        }
    }

    public void NextLevel()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex + 1);
    }

    public void MainMenu()
    {
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene("Main Menu");
    }
}

