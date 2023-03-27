using UnityEngine;
using Mirror;

public class StaminaManager : NetworkBehaviour
{
    [SyncVar]
    public int maxStamina = 100;
    [SyncVar]
    public int currentStamina;
    public float staminaRegenRate = 5f;
    private float staminaRegenTimer = 0f;

    void Start()
    {
        currentStamina = maxStamina;
    }

    void Update()
    {
        if (isServer)
        {
            HandleStaminaRegen();
        }
    }

    private void HandleStaminaRegen()
    {
        if (currentStamina < maxStamina)
        {
            staminaRegenTimer += Time.deltaTime;
            if (staminaRegenTimer >= 1f / staminaRegenRate)
            {
                staminaRegenTimer = 0f;
                RestoreStamina(1);
            }
        }
    }

    public void ReduceStamina(int amount)
    {
        if (!isServer) return;

        currentStamina -= amount;
        if (currentStamina < 0)
            currentStamina = 0;
    }

    public void RestoreStamina(int amount)
    {
        if (!isServer) return;

        currentStamina += amount;
        if (currentStamina > maxStamina)
            currentStamina = maxStamina;
    }

    public bool HasEnoughStamina(int amount)
    {
        return currentStamina >= amount;
    }
}