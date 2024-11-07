using UnityEngine;
using UnityEngine.UI;

public class PowerUpVelocity : MonoBehaviour
{
    public Image energyBar;
    public float maxEnergy = 100f;
    public float currentEnergy;
    public float energyDecrement = 10f;
    public float energyRechargeRate = 5f;
    public float speedIncreaseFactor = 2f;

    private bool isPowerUpActive = false;
    private bool isInvulnerable = false;
    private PlayerMovement playerMovement;

    void Start()
    {
        currentEnergy = maxEnergy;
        playerMovement = GetComponent<PlayerMovement>();
        UpdateEnergyBar();
    }

    void Update()
    {
        if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0)) && currentEnergy > 0 && !isPowerUpActive)
        {
            ActivatePowerUp();
        }

        if (isPowerUpActive)
        {
            ConsumeEnergy();
        }
        else
        {
            RechargeEnergy();
        }

        UpdateEnergyBar();
    }

    public void ActivatePowerUp()
    {
        isPowerUpActive = true;
        isInvulnerable = true;
        playerMovement.speed *= speedIncreaseFactor;
    }

    public void DeactivatePowerUp()
    {
        isPowerUpActive = false;
        isInvulnerable = false;
        playerMovement.speed /= speedIncreaseFactor;
    }

    private void ConsumeEnergy()
    {
        if (currentEnergy > 0)
        {
            currentEnergy -= energyDecrement * Time.deltaTime;
        }
        else
        {
            DeactivatePowerUp();
        }
    }

    private void RechargeEnergy()
    {
        if (currentEnergy < maxEnergy)
        {
            currentEnergy += energyRechargeRate * Time.deltaTime;
        }
    }

    private void UpdateEnergyBar()
    {
        energyBar.fillAmount = currentEnergy / maxEnergy;
    }
}
