using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;
    private GameManager gameManager; // Referencia al GameManager

    void Start()
    {
        currentHealth = maxHealth;

        // Buscar y asignar el GameManager en la escena
        gameManager = FindObjectOfType<GameManager>();
        if (gameManager == null)
        {
            Debug.LogError("No se encontró el GameManager en la escena.");
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Projectile"))
        {
            TakeDamage(10);
        }
    }

    void TakeDamage(int damage)
    {
        currentHealth -= damage;
        Debug.Log("Salud del enemigo: " + currentHealth);

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("El enemigo ha sido derrotado.");

        // Llamar a la función EndGame en el GameManager para mostrar la pantalla de victoria
        if (gameManager != null)
        {
            gameManager.EndGame(true);
        }

        Destroy(gameObject); // Destruir al enemigo después de activar la pantalla de victoria
        Time.timeScale = 0;
    }
}
