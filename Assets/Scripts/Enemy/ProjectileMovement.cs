using UnityEngine;

public class ProjectileMovement : MonoBehaviour
{
    private Vector3 direction;  // Direcci�n en la que se mover� el proyectil
    private float speed;        // Velocidad del proyectil

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime); // Movimiento en l�nea recta
    }

    // M�todo para configurar la direcci�n y velocidad
    public void SetDirection(Vector3 dir, float moveSpeed)
    {
        direction = dir;
        speed = moveSpeed;
    }
}
