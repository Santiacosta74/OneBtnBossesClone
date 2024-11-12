using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossProjectileAttack : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform spawnPoint; 
    public List<Transform> targetPointsWave1; 
    public List<Transform> targetPointsWave2; 
    public float spawnInterval = 0.1f; 
    public float projectileSpeed = 5f; 

    private float timer = 0f;
    private int currentTargetIndex = 0; 

    void Start()
    {
        StartCoroutine(SpawnWave1());
        StartCoroutine(SpawnWave2());
    }

    IEnumerator SpawnWave1()
    {
        yield return new WaitForSeconds(3f); 

        for (int i = 0; i < targetPointsWave1.Count; i++)
        {
            LaunchProjectile(targetPointsWave1[i]);
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    IEnumerator SpawnWave2()
    {
        yield return new WaitForSeconds(8f); 

        for (int i = 0; i < targetPointsWave2.Count; i++)
        {
            LaunchProjectile(targetPointsWave2[i]);
            yield return new WaitForSeconds(spawnInterval); 
        }
    }

    void LaunchProjectile(Transform targetPoint)
    {
        GameObject projectile = Instantiate(projectilePrefab, spawnPoint.position, Quaternion.identity);
        ProjectileMovement projectileMovement = projectile.GetComponent<ProjectileMovement>();

        if (projectileMovement != null && targetPoint != null)
        {
            projectileMovement.SetTarget(targetPoint.position, projectileSpeed);
        }
    }
}
