using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 direction;  // Dirección en la que se moverá el proyectil
    private float speed;        // Velocidad del proyectil

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // Movimiento en línea recta
    }

    // Método para configurar la dirección y velocidad
    public void SetDirection(Vector3 dir, float moveSpeed)
    {
        direction = dir;
        speed = moveSpeed;
    }
}
