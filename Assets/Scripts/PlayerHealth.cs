using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;
    private int currentLives;
    private GameManager gameManager; // Referencia al GameManager

    void Start()
    {
        currentLives = maxLives;

        // Buscar y asignar el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontr� el GameManager en la escena.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
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

        // Detener el tiempo de juego
        Time.timeScale = 0;

        // Llamar a la funci�n EndGame en el GameManager para mostrar la pantalla de derrota
        if (gameManager != null)
        {
            gameManager.EndGame(false);
        }
    }
}

