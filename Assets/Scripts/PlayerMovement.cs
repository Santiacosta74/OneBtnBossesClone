using UnityEngine;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    public PathPointsGenerator pathPointsGenerator;
    public float movementSpeed = 2f;
    private List<Vector2> pathPoints;
    private int currentPointIndex = 0;

    void Start()
    {
        pathPoints = pathPointsGenerator.pathPoints;
    }

    void Update()
    {
        if (pathPoints == null || pathPoints.Count == 0) return;

        // esto mueve hacia el siguiente punto en la lista
        Vector2 targetPosition = pathPoints[currentPointIndex];
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);

        // esto verifica si llegamos al punto actual, para pasar al siguiente
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = (currentPointIndex + 1) % pathPoints.Count;
        }
    }
}
