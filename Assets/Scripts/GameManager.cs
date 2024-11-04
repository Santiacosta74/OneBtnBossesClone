using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;
    public GameObject gameOverPanel;  // Panel de derrota
    public GameObject gameWinPanel;   // Panel de victoria
    public TextMeshProUGUI winTimeText;   // Texto para mostrar el tiempo en el panel de victoria
    public TextMeshProUGUI bestTimeText;  // Texto para mostrar el mejor tiempo

    private bool isGameStarted = false;
    private float bestTime = float.MaxValue;

    private GameTimer gameTimer;

    void Start()
    {
        ShowStartScreen();

        // Obtener el script GameTimer en la escena
        gameTimer = FindObjectOfType<GameTimer>();

        // Cargar el mejor tiempo guardado
        if (PlayerPrefs.HasKey("BestTime"))
        {
            bestTime = PlayerPrefs.GetFloat("BestTime");
        }
    }

    void Update()
    {
        if (!isGameStarted && Input.GetKeyDown(KeyCode.Space))
        {
            StartGame();
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

        // Detener el cronómetro al finalizar la partida
        gameTimer.StopTimer();
        float elapsedTime = gameTimer.GetElapsedTime();

        // Mostrar el panel correspondiente según el resultado
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

        // Mostrar el tiempo final en el panel de victoria
        winTimeText.text = "Tiempo: " + FormatTime(elapsedTime);

        // Verificar si es el mejor tiempo registrado
        if (elapsedTime < bestTime)
        {
            bestTime = elapsedTime;
            bestTimeText.text = "¡Nuevo mejor tiempo!";
            PlayerPrefs.SetFloat("BestTime", bestTime); // Guardar el mejor tiempo
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
}
