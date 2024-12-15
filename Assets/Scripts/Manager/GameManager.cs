using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public GameObject healthSliderPanel;
    public GameObject powerupPanel;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI bestTimeText;

    private bool isGameStarted = false;
    private bool isGameOver = false;
    private float bestTime = float.MaxValue;
    private GameTimer gameTimer;

    public AudioClip musicSource;
    [Range(0, 1)] public float musicVolume = 0.5f; // Volumen inicial de música

    void Start()
    {
        ShowStartScreen();
        Time.timeScale = 0;
        gameTimer = FindObjectOfType<GameTimer>();

        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
        else
        {
            bestTimeText.text = "";
        }
    }

    void Update()
    {
        if (!isGameStarted && !isGameOver && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
            Time.timeScale = 1;
        }
    }

    public void StartGame()
    {
        isGameStarted = true;
        startPanel.SetActive(false);
        healthSliderPanel.SetActive(true);
        powerupPanel.SetActive(true);
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicSource != null)
        {
            AudioManager.Instance.PlayMusic(musicSource);
            AudioManager.Instance.SetMusicVolume(musicVolume);
        }
    }

    public void EndGame(bool isVictory)
    {
        AudioManager.Instance.StopMusic();
        isGameStarted = false;
        isGameOver = true;
        gameTimer.StopTimer();
        float elapsedTime = gameTimer.GetElapsedTime();

        if (isVictory)
        {
            ShowWinScreen(elapsedTime);
        }
        else
        {
            ShowGameOverScreen();
        }
    }

    private void ShowWinScreen(float elapsedTime)
    {
        gameWinPanel.SetActive(true);
        healthSliderPanel.SetActive(false);
        winTimeText.text = "TIME:  " + FormatTime(elapsedTime);

        if (bestTime == float.MaxValue || elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            bestTimeText.text = "¡NEW BEST TIME!";
            PlayerPrefs.SetFloat("BestTime", bestTime);
        }
        else
        {
            bestTimeText.text = "BEST TIME:  " + FormatTime(bestTime);
        }
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
        healthSliderPanel.SetActive(false);
        powerupPanel.SetActive(false);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStartScreen()
    {
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
        healthSliderPanel.SetActive(false);
        powerupPanel.SetActive(false);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("LevelSelectScene"); // Reemplaza "MenuScene" con el nombre de tu escena de menú
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("BestTime");
    }
}
