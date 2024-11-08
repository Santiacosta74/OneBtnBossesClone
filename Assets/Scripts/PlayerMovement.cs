using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public PathPointsGenerator pathPointsGenerator;
    public float movementSpeed = 2f;
    public float startDelay = 1f; // Tiempo de espera antes de comenzar a moverse

    private List<Vector2> pathPoints;
    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool canMove = false; // Para controlar si el jugador puede moverse

    void Start()
    {
        pathPoints = pathPointsGenerator.pathPoints;
        StartCoroutine(WaitForPathPoints());
    }

    // Corrutina para esperar hasta que los pathPoints estén generados y pasar el tiempo de espera
    private IEnumerator WaitForPathPoints()
    {
        // Espera a que los pathPoints estén generados y luego espera el tiempo de retraso
        while (pathPoints == null || pathPoints.Count == 0)
        {
            pathPoints = pathPointsGenerator.pathPoints;
            yield return null;
        }

        yield return new WaitForSeconds(startDelay);
        canMove = true; // Permite el movimiento después de la espera
    }

    void Update()
    {
        if (!canMove || pathPoints == null || pathPoints.Count == 0) return;

        // Detección de cambio de dirección
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            movingForward = !movingForward;
            currentPointIndex = movingForward ? (currentPointIndex + 1) % pathPoints.Count
                                              : (currentPointIndex - 1 + pathPoints.Count) % pathPoints.Count;
        }
    }

    void FixedUpdate()
    {
        if (!canMove || pathPoints == null || pathPoints.Count == 0) return;

        // Movimiento en dirección al siguiente punto
        Vector2 targetPosition = pathPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.fixedDeltaTime);

        // Verificar si llegamos al punto actual para cambiar al siguiente
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = movingForward ? (currentPointIndex + 1) % pathPoints.Count
                                              : (currentPointIndex - 1 + pathPoints.Count) % pathPoints.Count;
        }
    }
}
