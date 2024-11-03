using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int maxLives = 3;  
    private int currentLives;   

    void Start()
    {
        currentLives = maxLives;  
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
    }
}
