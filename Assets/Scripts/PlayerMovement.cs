using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 2f; // Radio del movimiento circular
    public float speed = 2f;  // Velocidad de rotación

    private float angle = 0f;

    void Update()
    {
        // Aumenta el ángulo según la velocidad
        angle += speed * Time.deltaTime;

        // Calcula la posición del jugador en el círculo
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector2(x, y);
    }
}
