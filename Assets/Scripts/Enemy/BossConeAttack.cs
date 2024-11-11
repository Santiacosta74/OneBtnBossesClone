using UnityEngine;

public class BossConeAttack : MonoBehaviour
{
    public GameObject conePrefab; 
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

        GameObject cone = Instantiate(conePrefab, spawnPosition, Quaternion.Euler(0f, 0f, randomAngle));
        Destroy(cone, 2f);
    }
}
