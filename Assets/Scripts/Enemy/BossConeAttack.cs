using UnityEngine;

public class BossConeAttack : MonoBehaviour
{
    public ObjectPool conePool;
    public float spawnInterval = 5f;
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
            SpawnCone();
            timer = 0f;
        }
    }

    void SpawnCone()
    {
        if (pathPointsGenerator.pathPoints.Count == 0) return;

        int randomIndex = Random.Range(0, pathPointsGenerator.pathPoints.Count);
        Vector2 spawnPosition = pathPointsGenerator.pathPoints[randomIndex];

        float randomAngle = Random.Range(0f, 360f);
        GameObject cone = conePool.GetObject();
        cone.transform.position = spawnPosition;
        cone.transform.rotation = Quaternion.Euler(0f, 0f, randomAngle);

        StartCoroutine(ReturnToPool(cone, 2f));
    }

    private System.Collections.IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        conePool.ReturnObject(obj);
    }
}
