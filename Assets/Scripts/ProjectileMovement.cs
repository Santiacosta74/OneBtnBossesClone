using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float projectileSpeed = 10f;

    void Start()
    {
        Vector2 direction = (Vector2.zero - (Vector2)transform.position).normalized;

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
            Destroy(gameObject);
        }
    }
}