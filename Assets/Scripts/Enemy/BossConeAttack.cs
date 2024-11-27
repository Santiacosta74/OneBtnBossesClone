using UnityEngine;

public class BossConeAttack : MonoBehaviour
{
    public AttackFactoryEnemy attackFactory;
    public PathPointsGenerator pathPointsGenerator;
    public float spawnInterval = 5f;
    private float timer = 0f;
    public ObjectPool conePool; // Referencia al Object Pool de flechas

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

        GameObject cone = attackFactory.CreateCone(spawnPosition);

        StartCoroutine(ReturnToPool(cone, 2f));
    }

    private System.Collections.IEnumerator ReturnToPool(GameObject obj, float delay)
    {
        yield return new WaitForSeconds(delay);
        conePool.ReturnObject(obj);
    }
}
