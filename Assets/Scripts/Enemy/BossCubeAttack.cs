using UnityEngine;

public class BossCubeAttack : MonoBehaviour
{
    public GameObject obstaclePrefab;
    public float spawnInterval = 3f;
    public PathPointsGenerator pathPointsGenerator;
    private float timer = 0f;

    void Start()
    {
        if (pathPointsGenerator == null)
        {
            pathPointsGenerator = FindObjectOfType<PathPointsGenerator>();
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval)
        {
            SpawnObstacle();
            timer = 0f;
        }
    }

    void SpawnObstacle()
    {
        if (pathPointsGenerator.pathPoints.Count < 2) return;

        int randomIndex = Random.Range(0, pathPointsGenerator.pathPoints.Count);
        Vector2 point1 = pathPointsGenerator.pathPoints[randomIndex];
        Vector2 point2 = pathPointsGenerator.pathPoints[(randomIndex + 1) % pathPointsGenerator.pathPoints.Count];

        float t = Random.Range(0f, 1f);
        Vector2 spawnPosition = Vector2.Lerp(point1, point2, t);

        float randomAngle = Random.Range(0f, 360f);
        Quaternion randomRotation = Quaternion.Euler(0f, 0f, randomAngle);

        GameObject obstacle = Instantiate(obstaclePrefab, spawnPosition, randomRotation);

        Collider2D obstacleCollider = obstacle.GetComponent<Collider2D>();
        if (obstacleCollider != null)
        {
            obstacleCollider.enabled = true;
        }

        Destroy(obstacle, 3f);
    }
}
