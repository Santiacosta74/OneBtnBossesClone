using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class PlayerMovement2 : MonoBehaviour
{
    public PathPointsGenerator pathPointsGenerator;
    public float movementSpeed = 2f; // Velocidad normal de movimiento
    public float startDelay = 1f; // Tiempo de espera antes de comenzar a moverse
    public float dashSpeed = 4f; // Velocidad cuando el dash está activo
    public float energyRechargeRate = 5f; // Tasa de recarga de energía por segundo
    public float energyDrainRate = 10f; // Tasa de consumo de energía por segundo durante el dash
    public Slider energyBar; // Barra de energía en la UI (debes configurarlo en el inspector)

    private List<Vector2> pathPoints;
    private int currentPointIndex = 0;
    private bool movingForward = true;
    private bool canMove = false; // Para controlar si el jugador puede moverse
    private bool isDashing = false; // Para verificar si el dash está activo
    private bool isChargingEnergy = false; // Para saber si la energía está siendo recargada
    private float currentEnergy = 0f; // Energía inicial del jugador (arranca en 0)

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
        // Espera a que los pathPoints estén generados y luego espera el tiempo de retraso
        while (pathPoints == null || pathPoints.Count == 0)
        {
            pathPoints = pathPointsGenerator.pathPoints;
            yield return null;
        }

        yield return new WaitForSeconds(startDelay); // Espera 1 segundo antes de permitir el movimiento
        canMove = true; // Permite el movimiento después del delay
    }

    void Update()
    {
        if (!canMove || pathPoints == null || pathPoints.Count == 0) return;

        // Si el dash no está activo y no estamos cargando energía, recargamos la energía
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

        // Debug log para mostrar la energía
        //Debug.Log("Energía actual: " + currentEnergy);

        // Actualizamos la barra de energía
        if (energyBar != null)
        {
            energyBar.value = currentEnergy / 100f;
        }

        // Si el jugador mantiene presionado el clic izquierdo o la barra espaciadora, y tiene energía disponible
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
        float currentSpeed = isDashing ? dashSpeed : movementSpeed; // Si está en dash, aumenta la velocidad
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
        isChargingEnergy = true; // Evita que la energía se recargue mientras haces el dash

        // Durante el dash, se consume energía mientras el jugador mantiene el clic o la barra espaciadora
        while ((Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)) && currentEnergy > 0 && isDashing)
        {
            currentEnergy -= energyDrainRate * Time.deltaTime; // Consume energía
            if (currentEnergy <= 0)
            {
                currentEnergy = 0;
                isDashing = false; // Detenemos el dash cuando se agota la energía
                break; // Salimos del bucle si la energía se agotó
            }

            // Debug log para mostrar la energía mientras se hace el dash
            //Debug.Log("Energía durante el Dash: " + currentEnergy);

            yield return null;
        }

        // Detener el dash si no se presiona espacio o clic
        if (!Input.GetMouseButton(0) && !Input.GetKey(KeyCode.Space))
        {
            isDashing = false;
        }

        // Después de que el dash se detiene, dejamos de cargar energía hasta que se deje de presionar el botón
        while (currentEnergy < 100f && (Input.GetMouseButton(0) || Input.GetKey(KeyCode.Space)))
        {
            // No recargamos energía hasta que no se presione el botón
            yield return null;
        }

        // Cuando se haya dejado de presionar el espacio o clic, comenzamos a recargar la energía
        isChargingEnergy = false; // Permite que la energía se recargue después de soltar el botón

        isDashing = false; // El dash ha terminado
    }

    // Función para comprobar si el jugador está invulnerable durante el Dash
    public bool IsInvulnerable()
    {
        return isDashing;
    }
}
