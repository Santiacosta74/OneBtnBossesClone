using UnityEngine;
using System.Collections;

public class EnemyShooting : MonoBehaviour
{
    public ObjectPool projectilePool; // Referencia al pool de proyectiles
    public Transform firePoint;
    public float shootInterval = 2f;
    public float projectileSpeed = 5f;
    private float nextShootTime = 0f;

    void Update()
    {
        if (Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        // Obtener proyectil del pool
        GameObject projectile = projectilePool.GetObject();
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        // Asociar el ObjectPool con el proyectil
        Projectile projectileComponent = projectile.GetComponent<Projectile>();
        if (projectileComponent != null)
        {
            projectileComponent.objectPool = projectilePool; // Asegurar la referencia
        }

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            // Calcular dirección aleatoria para el disparo
            float randomAngle = Random.Range(0f, 360f);
            Vector2 direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;
            rb.velocity = direction * projectileSpeed;
        }

        // Iniciar corutina para devolver al pool
        StartCoroutine(ReturnProjectileToPool(projectile, 3f));
    }

    private IEnumerator ReturnProjectileToPool(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Verificar que el objeto no haya sido destruido
        if (projectile != null && projectile.activeInHierarchy)
        {
            projectilePool.ReturnObject(projectile);
        }
    }
}
