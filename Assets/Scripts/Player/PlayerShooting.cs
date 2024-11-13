using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public ObjectPool projectilePool; // Referencia al pool de proyectiles
    public Transform firePoint;         
    public float shootInterval = 1f;
    public float startDelay = 2f;
    private float nextShootTime = 0f;
    private float startTime;
    void Start()
    {
        startTime = Time.time + startDelay; // Calcula el tiempo en que comienza a disparar
    }
    void Update()
    {
        if (Time.time >= startTime && Time.time >= nextShootTime)
        {
            Shoot();
            nextShootTime = Time.time + shootInterval;
        }
    }

    void Shoot()
    {
        GameObject projectile = projectilePool.GetObject(); 
        projectile.transform.position = firePoint.position; 
        projectile.transform.rotation = firePoint.rotation;

        projectile.GetComponent<Projectile>().objectPool = projectilePool;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            Vector2 direction = (Vector2.zero - (Vector2)projectile.transform.position).normalized;
            rb.velocity = direction * projectile.GetComponent<Projectile>().projectileSpeed;
        }

        StartCoroutine(ReturnProjectileToPool(projectile, 3f)); 
    }
    private System.Collections.IEnumerator ReturnProjectileToPool(GameObject projectile, float delay)
    {
        yield return new WaitForSeconds(delay);
        projectilePool.ReturnObject(projectile);
    }
}
