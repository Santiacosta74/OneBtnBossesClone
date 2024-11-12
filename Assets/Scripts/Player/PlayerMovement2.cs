using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement2 : MonoBehaviour
{
    public PathPointsGenerator pathPointsGenerator;
    public float movementSpeed = 2f; 
    public float startDelay = 1f; 
    public float dashSpeed = 4f; 
    public float energyRechargeRate = 5f; 
    public float energyDrainRate = 10f; 
    public Slider energyBar; 
    private List<Vector2> pathPoints;
    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool canMove = false; 
    private bool isDashing = false; 
    private bool isChargingEnergy = false; 
    private float currentEnergy = 0f; 

    private PlayerHealth2 PlayerHealth2; 

    void Start()
    {
        pathPoints = pathPointsGenerator.pathPoints;
        StartCoroutine(WaitForPathPoints());

        PlayerHealth2 = GetComponent<PlayerHealth2>();
    }

    private IEnumerator WaitForPathPoints()
    {
        while (pathPoints == null || pathPoints.Count == 0)
        {
            pathPoints = pathPointsGenerator.pathPoints;
            yield return null;
        }

        yield return new WaitForSeconds(startDelay); 
        canMove = true; 
    }

    void Update()
    {
        if (!canMove || pathPoints == null || pathPoints.Count == 0) return;

        if (!isDashing && !isChargingEnergy)
        {
            if (currentEnergy < 100f)
            {
                currentEnergy += energyRechargeRate * Time.deltaTime;
                if (currentEnergy > 100f)
                {
                    currentEnergy = 100f;
                }
            }
        }

        if (energyBar != null)
        {
            energyBar.value = currentEnergy / 100f;
        }

        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && currentEnergy > 0 && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!canMove || pathPoints == null || pathPoints.Count == 0) return;

        Vector2 targetPosition = pathPoints[currentPointIndex];
        float currentSpeed = isDashing ? dashSpeed : movementSpeed; 
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentSpeed * Time.fixedDeltaTime);

        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = movingForward ? (currentPointIndex + 1) % pathPoints.Count
                                              : (currentPointIndex - 1 + pathPoints.Count) % pathPoints.Count;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        isChargingEnergy = true; 

        while ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && currentEnergy > 0 && isDashing)
        {
            currentEnergy -= energyDrainRate * Time.deltaTime; 
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                isDashing = false; 
                break; 
            }

            yield return null;
        }

        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
        {
            isDashing = false;
        }

        while (currentEnergy < 100f && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            yield return null;
        }

        isChargingEnergy = false; 

        isDashing = false; 
    }

    public bool IsInvulnerable()
    {
        return isDashing;
    }
}
