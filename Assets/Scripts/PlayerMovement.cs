using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 2f; // Radio del movimiento circular
    public float speed = 2f;  // Velocidad de rotaci�n

    private float angle = 0f;

    void Update()
    {
        // Aumenta el �ngulo seg�n la velocidad
        angle += speed * Time.deltaTime;

        // Calcula la posici�n del jugador en el c�rculo
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector2(x, y);
    }
}
