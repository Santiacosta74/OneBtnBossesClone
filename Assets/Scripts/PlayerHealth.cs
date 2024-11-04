using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
<<<<<<< HEAD
    public int maxLives = 3;  
    private int currentLives;   
    private GameManager gameManager;
    void Start()
    {
        currentLives = maxLives;
        gameManager = FindObjectOfType<GameManager>();
=======
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
            Debug.LogError("No se encontró el GameManager en la escena.");
        }
>>>>>>> Victory_And_Defeat_Poster
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("EnemyProjectile"))
        {
            TakeDamage(1);
            Destroy(collision.gameObject);
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
<<<<<<< HEAD
        gameManager.EndGame(false);
=======

        // Detener el tiempo de juego
        Time.timeScale = 0;

        // Llamar a la función EndGame en el GameManager para mostrar la pantalla de derrota
        if (gameManager != null)
        {
            gameManager.EndGame(false);
        }
>>>>>>> Victory_And_Defeat_Poster
    }
}

