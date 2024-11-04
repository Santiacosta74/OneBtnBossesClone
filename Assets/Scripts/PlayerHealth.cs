using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private GameManager gameManager;

    void Start()
    {
        currentLives = maxLives;

        gameManager = FindObjectOfType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject); // Destruir el proyectil al colisionar
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
