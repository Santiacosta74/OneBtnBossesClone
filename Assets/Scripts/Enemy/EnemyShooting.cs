using UnityEngine;

public class EnemyShooting : MonoBehaviour
{
    public ObjectPool objectPool; // Referencia al Object Pool
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
        // Obtener el proyectil desde el Object Pool
        GameObject projectile = objectPool.GetObject();
        projectile.transform.position = firePoint.position;
        projectile.transform.rotation = firePoint.rotation;

        float randomAngle = Random.Range(0f, 360f);
        Vector2 direction = new Vector2(Mathf.Cos(randomAngle), Mathf.Sin(randomAngle)).normalized;

        Rigidbody2D rb = projectile.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.velocity = direction * projectileSpeed;
        }

        // Retornar el proyectil al pool después de un tiempo
        StartCoroutine(ReturnToPoolAfterTime(projectile, 3f)); // Ajusta el tiempo según sea necesario
    }

    private System.Collections.IEnumerator ReturnToPoolAfterTime(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        objectPool.ReturnObject(obj);
    }
}
