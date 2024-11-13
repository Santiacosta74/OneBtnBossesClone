using UnityEngine;

public class ArrowsMovement : MonoBehaviour
{
    private Vector3 targetPosition;
    private float speed;

    public void SetTarget(Vector3 target, float moveSpeed)
    {
        targetPosition = target;
        speed = moveSpeed;

        Vector3 direction = targetPosition - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);


    }
}
