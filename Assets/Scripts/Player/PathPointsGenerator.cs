using System.Collections.Generic;
using UnityEngine;

public class PathPointsGenerator : MonoBehaviour
{
    public float radius = 4f;
    public int numberOfPoints = 50;
    public List<Vector2> pathPoints;

    void Start()
    {
        GenerateCircularPath();
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
