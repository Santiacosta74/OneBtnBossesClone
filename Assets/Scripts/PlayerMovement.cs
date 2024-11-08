using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public PathPointsGenerator pathPointsGenerator;
    public float movementSpeed = 2f;
    private List<Vector2> pathPoints;
    private int currentPointIndex = 0;
    private bool movingForward = true;

    void Start()
    {
        pathPoints = pathPointsGenerator.pathPoints;
    }

    void Update()
    {
        // Detección de cambio de dirección en cada frame
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            movingForward = !movingForward;
            // Ajuste inmediato de dirección en el índice
            currentPointIndex = movingForward ? (currentPointIndex + 1) % pathPoints.Count
                                              : (currentPointIndex - 1 + pathPoints.Count) % pathPoints.Count;
        }
    }

    void FixedUpdate()
    {
        if (pathPoints == null || pathPoints.Count == 0) return;

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
