using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100;  
    private int currentHealth;   

    void Start()
    {
        currentHealth = maxHealth; 
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
        Destroy(gameObject); 
    }
}
