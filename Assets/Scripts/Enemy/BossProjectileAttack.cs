using UnityEngine;

public class BossProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public float spawnInterval = 1f;  // Intervalo de tiempo para cada proyectil
    public float projectileSpeed = 5f; // Velocidad del proyectil
    public float attackDuration = 3f;  // Duración de la invocación del ataque
    private float timer = 0f;
    private float attackTimer = 0f;

    void Update()
    {
        if (attackTimer < attackDuration)
        {
            attackTimer += Time.deltaTime;
            timer += Time.deltaTime;

            if (timer >= spawnInterval)
            {
                SpawnProjectile();
                timer = 0f;
            }
        }
    }

    void SpawnProjectile()
    {
        // Instancia el proyectil cerca de la posición del jefe
        Vector3 spawnPosition = transform.position + new Vector3(1f, 0f, 0f); // Puedes ajustar la posición

        GameObject projectile = Instantiate(projectilePrefab, spawnPosition, Quaternion.identity);

        // Configura la dirección del proyectil en línea recta (hacia la derecha)
        Vector3 direction = Vector3.right;

        // Llama a la función para mover el proyectil
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();
        if (projectileMovement != null)
        {
            projectileMovement.SetDirection(direction, projectileSpeed);
        }
    }
}
