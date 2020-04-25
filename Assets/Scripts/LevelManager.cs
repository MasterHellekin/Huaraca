using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;


public class LevelManager : MonoBehaviour
{
    // Config
    [SerializeField] float waitToRespawn;
    [SerializeField] float waitToRestart;
    [SerializeField] public int coinCount;
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] int startingLives;
    [SerializeField] public int currentLives;
    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] GameObject gameOverScreen;
    [SerializeField] GameObject deathExplosion;
    [SerializeField] public AudioSource levelMusic;
    [SerializeField] AudioSource gameOverMusic;

    // State
    private int coinBonusLifeCount;

    //Cached component references
    Player player;

    // Start is called before the first frame update
    void Start()
    {
        player = FindObjectOfType<Player>();

        if (PlayerPrefs.HasKey("coinCount"))
        {
            coinCount = PlayerPrefs.GetInt("coinCount");
        }

        coinText.text = "x " + coinCount.ToString();

        if (PlayerPrefs.HasKey("playerLives"))
        {
            currentLives = PlayerPrefs.GetInt("playerLives");
        }
        else
        {
            currentLives = startingLives;
        }

        livesText.text = "x " + currentLives;
    }

    // Update is called once per frame
    void Update()
    {
        if (coinBonusLifeCount >= 100)
        {
            currentLives += 1;
            livesText.text = "x " + currentLives;
            coinBonusLifeCount -= 100;
        }
    }

    public void Respawn()
    {
        currentLives -= 1;
        livesText.text = "x " + currentLives;
        if (currentLives > 0)
        {
            StartCoroutine("ProcessPlayerDeath");
        }
        else
        {
            StartCoroutine("ProcessPlayerRestart");
        }

    }

    public IEnumerator ProcessPlayerDeath()
    {
        player.GetComponent<Animator>().SetTrigger("Dying");
        player.isAlive = false;
        Instantiate(deathExplosion, player.transform.position, player.transform.rotation);
        yield return new WaitForSeconds(waitToRespawn);
        player.isAlive = true;
        coinCount = 0;
        coinText.text = "x " + coinCount.ToString();
        coinBonusLifeCount = 0;
        player.transform.position = player.respawnPosition;
        player.GetComponent<Animator>().SetTrigger("Idleing");
    }

    public IEnumerator ProcessPlayerRestart()
    {
        player.gameObject.SetActive(false);
        gameOverScreen.SetActive(true);
        levelMusic.Stop();
        gameOverMusic.Play();
        yield return new WaitForSeconds(waitToRestart);
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    public void AddCoins(int coinToAdd)
    {
        coinCount += coinToAdd;
        coinBonusLifeCount += coinToAdd;
        coinText.text = "x " + coinCount.ToString();
    }

    public void AddLives(int livesToAdd)
    {
        currentLives += livesToAdd;
        livesText.text = "x " + currentLives;
    }
}
