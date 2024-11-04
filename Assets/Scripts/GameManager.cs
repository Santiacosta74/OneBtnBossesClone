using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;
    public GameObject gameWinPanel;
    public TextMeshProUGUI winTimeText;
    public TextMeshProUGUI bestTimeText;

    private bool isGameStarted = false;
    private float bestTime = float.MaxValue;
    private GameTimer gameTimer;

    void Start()
    {
        ShowStartScreen();
        Time.timeScale = 0;  // Pausar el juego en la pantalla de inicio
        gameTimer = FindObjectOfType<GameTimer>();

        // Cargar el mejor tiempo guardado solo si existe
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
        else
        {
            bestTimeText.text = ""; // No mostrar nada si no hay un tiempo guardado
        }
    }

    void Update()
    {
        // Iniciar el juego al presionar la barra espaciadora
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
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
        winTimeText.text = "Tiempo: " + FormatTime(elapsedTime);

        // Si es la primera vez o se establece un nuevo mejor tiempo, se guarda
        if (bestTime == float.MaxValue || elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            bestTimeText.text = "¡Nuevo mejor tiempo!";
            PlayerPrefs.SetFloat("BestTime", bestTime);  // Guardar el nuevo mejor tiempo
        }
        else
        {
            bestTimeText.text = "Mejor tiempo: " + FormatTime(bestTime);
        }
    }

    private void ShowGameOverScreen()
    {
        gameOverPanel.SetActive(true);
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
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
        // Resetear el mejor tiempo al salir del juego
        PlayerPrefs.DeleteKey("BestTime");
    }
}
