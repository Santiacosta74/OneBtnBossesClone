using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class PathPointsGenerator : MonoBehaviour
{
    public float radius = 4f;
    public int numberOfPoints = 50;
    public List<Vector2> pathPoints;
    private LineRenderer lineRenderer;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        GenerateCircularPath();
        DrawPathLines();
    }

    void GenerateCircularPath()
    {
        pathPoints = new List<Vector2>();
        for (int i = 0; i < numberOfPoints; i++)
        {
            float angle = (2 * Mathf.PI / numberOfPoints) * i;
            float x = Mathf.Cos(angle) * radius;
            float y = Mathf.Sin(angle) * radius;
            pathPoints.Add(new Vector2(x, y));
        }
    }

    void DrawPathLines()
    {
        lineRenderer.positionCount = pathPoints.Count + 1; // Para cerrar el círculo
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.loop = true; // Para cerrar el círculo

        for (int i = 0; i < pathPoints.Count; i++)
        {
            lineRenderer.SetPosition(i, new Vector3(pathPoints[i].x, pathPoints[i].y, 0));
        }
        // Conectar el último punto con el primero
        lineRenderer.SetPosition(pathPoints.Count, new Vector3(pathPoints[0].x, pathPoints[0].y, 0));
    }

    void OnDrawGizmos()
    {
        if (pathPoints == null) return;

        Gizmos.color = Color.red;
        foreach (Vector2 point in pathPoints)
        {
            Gizmos.DrawSphere(new Vector3(point.x, point.y, 0), 0.1f);
        }
    }
}
