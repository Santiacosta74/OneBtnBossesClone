using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    public GameObject projectilePrefab; 
    public Transform firePoint;         
    public float shootInterval = 1f;     

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
        Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
    }
}
