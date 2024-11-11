using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement2 : MonoBehaviour
{
    public PathPointsGenerator pathPointsGenerator;
    public float movementSpeed = 2f; // Velocidad normal de movimiento
    public float startDelay = 1f; // Tiempo de espera antes de comenzar a moverse
    public float dashSpeed = 4f; // Velocidad cuando el dash est� activo
    public float energyRechargeRate = 5f; // Tasa de recarga de energ�a por segundo
    public float energyDrainRate = 10f; // Tasa de consumo de energ�a por segundo durante el dash
    public Slider energyBar; // Barra de energ�a en la UI (debes configurarlo en el inspector)

    private List<Vector2> pathPoints;
    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool canMove = false; // Para controlar si el jugador puede moverse
    private bool isDashing = false; // Para verificar si el dash est� activo
    private bool isChargingEnergy = false; // Para saber si la energ�a est� siendo recargada
    private float currentEnergy = 0f; // Energ�a inicial del jugador (arranca en 0)

    private PlayerHealth2 playerHealth; // Referencia al script PlayerHealth

    void Start()
    {
        pathPoints = pathPointsGenerator.pathPoints;
        StartCoroutine(WaitForPathPoints());

        // Encontramos el componente PlayerHealth en el mismo objeto
        playerHealth = GetComponent<PlayerHealth2>();
    }

    private IEnumerator WaitForPathPoints()
    {
        // Espera a que los pathPoints est�n generados y luego espera el tiempo de retraso
        while (pathPoints == null || pathPoints.Count == 0)
        {
            pathPoints = pathPointsGenerator.pathPoints;
            yield return null;
        }

        yield return new WaitForSeconds(startDelay); // Espera 1 segundo antes de permitir el movimiento
        canMove = true; // Permite el movimiento despu�s del delay
    }

    void Update()
    {
        if (!canMove || pathPoints == null || pathPoints.Count == 0) return;

        // Si el dash no est� activo y no estamos cargando energ�a, recargamos la energ�a
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

        // Debug log para mostrar la energ�a
        //Debug.Log("Energ�a actual: " + currentEnergy);

        // Actualizamos la barra de energ�a
        if (energyBar != null)
        {
            energyBar.value = currentEnergy / 100f;
        }

        // Si el jugador mantiene presionado el clic izquierdo o la barra espaciadora, y tiene energ�a disponible
        if ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && currentEnergy > 0 && !isDashing)
        {
            StartCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (!canMove || pathPoints == null || pathPoints.Count == 0) return;

        // Movimiento normal o con dash
        Vector2 targetPosition = pathPoints[currentPointIndex];
        float currentSpeed = isDashing ? dashSpeed : movementSpeed; // Si est� en dash, aumenta la velocidad
        transform.position = Vector2.MoveTowards(transform.position, targetPosition, currentSpeed * Time.fixedDeltaTime);

        // Verificar si llegamos al punto actual para cambiar al siguiente
        if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
        {
            currentPointIndex = movingForward ? (currentPointIndex + 1) % pathPoints.Count
                                              : (currentPointIndex - 1 + pathPoints.Count) % pathPoints.Count;
        }
    }

    private IEnumerator Dash()
    {
        isDashing = true;
        isChargingEnergy = true; // Evita que la energ�a se recargue mientras haces el dash

        // Durante el dash, se consume energ�a mientras el jugador mantiene el clic o la barra espaciadora
        while ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && currentEnergy > 0 && isDashing)
        {
            currentEnergy -= energyDrainRate * Time.deltaTime; // Consume energ�a
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                isDashing = false; // Detenemos el dash cuando se agota la energ�a
                break; // Salimos del bucle si la energ�a se agot�
            }

            // Debug log para mostrar la energ�a mientras se hace el dash
            //Debug.Log("Energ�a durante el Dash: " + currentEnergy);

            yield return null;
        }

        // Detener el dash si no se presiona espacio o clic
        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
        {
            isDashing = false;
        }

        // Despu�s de que el dash se detiene, dejamos de cargar energ�a hasta que se deje de presionar el bot�n
        while (currentEnergy < 100f && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            // No recargamos energ�a hasta que no se presione el bot�n
            yield return null;
        }

        // Cuando se haya dejado de presionar el espacio o clic, comenzamos a recargar la energ�a
        isChargingEnergy = false; // Permite que la energ�a se recargue despu�s de soltar el bot�n

        isDashing = false; // El dash ha terminado
    }

    // Funci�n para comprobar si el jugador est� invulnerable durante el Dash
    public bool IsInvulnerable()
    {
        return isDashing;
    }
}
