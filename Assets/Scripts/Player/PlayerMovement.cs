using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float radius = 4f; 
    public float speed = 2f;  

    private float angle = 0f;

    void Update()
    {
        angle += speed * Time.deltaTime;
 
        float x = Mathf.Cos(angle) * radius;
        float y = Mathf.Sin(angle) * radius;

        transform.position = new Vector2(x, y);

        if (Input.GetKeyDown(KeyCode.Space))
        {
            speed = -speed; 
        }

    }
}
