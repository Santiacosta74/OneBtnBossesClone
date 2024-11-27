using UnityEngine;

public class AttackFactoryEnemy : MonoBehaviour
{
    public ObjectPool obstaclePool;
    public ObjectPool conePool;
    public ObjectPool arrowsPool;

    public GameObject CreateObstacle(Vector2 spawnPosition)
    {
        GameObject obstacle = obstaclePool.GetObject();
        obstacle.transform.position = spawnPosition;
        obstacle.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return obstacle;
    }

    public GameObject CreateCone(Vector2 spawnPosition)
    {
        GameObject cone = conePool.GetObject();
        cone.transform.position = spawnPosition;
        cone.transform.rotation = Quaternion.Euler(0f, 0f, Random.Range(0f, 360f));
        return cone;
    }

    public GameObject CreateArrow(Vector2 spawnPosition, Vector2 target, float speed)
    {
        GameObject arrow = arrowsPool.GetObject();
        arrow.transform.position = spawnPosition;
        arrow.transform.rotation = Quaternion.identity;
        ArrowsMovement movement = arrow.GetComponent<ArrowsMovement>();
        if (movement != null)
        {
            movement.SetTarget(target, speed);
        }
        return arrow;
    }
}
