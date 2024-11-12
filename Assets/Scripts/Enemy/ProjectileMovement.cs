using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed;

    // Configurar el objetivo y velocidad del proyectil
    public void SetTarget(Vector3 target, float moveSpeed)
    {
        targetPosition = target;
        speed = moveSpeed;

        // Rotar el proyectil hacia el objetivo
        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        // Movimiento hacia el objetivo
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

        // Destruir el proyectil si alcanza el objetivo
        if (Vector3.Distance(transform.position, targetPosition) < 0.1f)
        {
            Destroy(gameObject);
        }
    }
}
