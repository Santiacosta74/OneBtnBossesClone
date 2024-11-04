using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI bestTimeText;

    private bool isGameStarted = false;
    private bool isGameOver = false; // Nueva variable
    private float bestTime = float.MaxValue;
    private GameTimer gameTimer;

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
        // Solo permite iniciar el juego si no ha empezado y no ha terminado
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
    }

    public void EndGame(bool isVictory)
    {
        isGameStarted = false;
        isGameOver = true; // Indica que el juego ha terminado
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
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        isGameOver = false; // Resetea el estado de juego terminado al reiniciar
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void ShowStartScreen()
    {
        startPanel.SetActive(true);
        gameOverPanel.SetActive(false);
        gameWinPanel.SetActive(false);
    }

    private string FormatTime(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    void OnApplicationQuit()
    {
        PlayerPrefs.DeleteKey("BestTime");
    }
}
