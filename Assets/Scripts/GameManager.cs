using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject startPanel;    
    public GameObject gameOverPanel; 

    private bool isGameStarted = false;

    void Start()
    {
        ShowStartScreen();
        Time.timeScale = 0; 
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
        Time.timeScale = 1; 
    }

    public void EndGame(bool isVictory)
    {
        isGameStarted = false;
        gameOverPanel.SetActive(true);
        Time.timeScale = 0; 
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
        Time.timeScale = 0; 
    }
}
