using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab; // Prefab del proyectil
    public Transform spawnPoint; // Punto de spawn del proyectil
    public List<Transform> targetPointsWave1; // Lista de puntos objetivo para la primera ola
    public List<Transform> targetPointsWave2; // Lista de puntos objetivo para la segunda ola
    public float spawnInterval = 0.1f; // Intervalo de tiempo entre proyectiles
    public float projectileSpeed = 5f; // Velocidad del proyectil

    private float timer = 0f;
    private int currentTargetIndex = 0; // Índice del objetivo actual

    void Start()
    {
        // Iniciar corutinas para las dos olas
        StartCoroutine(SpawnWave1());
        StartCoroutine(SpawnWave2());
    }

    // Corutina para la primera ola de proyectiles (después de 4 segundos)
    IEnumerator SpawnWave1()
    {
        yield return new WaitForSeconds(3f); // Espera de 4 segundos

        for (int i = 0; i < targetPointsWave1.Count; i++)
        {
            LaunchProjectile(targetPointsWave1[i]);
            yield return new WaitForSeconds(spawnInterval); // Intervalo entre cada disparo
        }
    }

    // Corutina para la segunda ola de proyectiles (después de 10 segundos)
    IEnumerator SpawnWave2()
    {
        yield return new WaitForSeconds(8f); // Espera de 10 segundos

        for (int i = 0; i < targetPointsWave2.Count; i++)
        {
            LaunchProjectile(targetPointsWave2[i]);
            yield return new WaitForSeconds(spawnInterval); // Intervalo entre cada disparo
        }
    }

    // Método para disparar proyectil hacia un objetivo específico
    void LaunchProjectile(Transform targetPoint)
    {
        // Instanciar el proyectil
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();

        if (projectileMovement != null && targetPoint != null)
        {
            projectileMovement.SetTarget(targetPoint.position, projectileSpeed);
        }
    }
}
