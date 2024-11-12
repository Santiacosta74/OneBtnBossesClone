using UnityEngine;

public class PlayerHealth2 : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private GameManager gameManager;
    private PlayerMovement2 playerMovement;

    void Start()
    {
        currentLives = maxLives;
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = GetComponent<PlayerMovement2>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (playerMovement != null && !playerMovement.IsInvulnerable() && collision.CompareTag("Obstacle") || playerMovement != null && !playerMovement.IsInvulnerable() && collision.CompareTag("Arrow"))
        {
            TakeDamage(1);
        }
    }

    void TakeDamage(int damage)
    {
        currentLives -= damage;
        Debug.Log("Vidas del jugador restantes: " + currentLives);

        if (currentLives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        Debug.Log("Game Over: El jugador ha perdido todas las vidas.");
        gameManager.EndGame(false);
        Time.timeScale = 0;
    }
}
