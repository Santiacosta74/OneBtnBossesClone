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
        gameOverPanel.SetActive(true); 
        // Nacho aca podes mostrar los detalles de victoria o derrota
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); 
    }

    private void ShowStartScreen()
    {
        startPanel.SetActive(true);   
        gameOverPanel.SetActive(false); 
    }
}
