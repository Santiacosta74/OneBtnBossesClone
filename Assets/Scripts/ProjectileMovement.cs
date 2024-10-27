using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f;  // Velocidad del proyectil
    private Vector2 targetPosition;

    void Start()
    {
        // Establece el centro de la pantalla (donde est� el enemigo) como objetivo
        targetPosition = Vector2.zero;  // Ajusta seg�n la posici�n exacta del enemigo si no est� en (0, 0)

        // Calcula la direcci�n hacia el centro
        Vector2 direction = (targetPosition - (Vector2)transform.position).normalized;

        // Asigna la velocidad hacia el objetivo
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            Destroy(gameObject);  // Destruye el proyectil al colisionar con el enemigo
        }
    }
}
