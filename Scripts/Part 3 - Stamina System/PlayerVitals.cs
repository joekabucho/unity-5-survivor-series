using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityStandardAssets.Characters.FirstPerson; //NEW

public class PlayerVitals : MonoBehaviour
{
    public Slider healthSlider;
    public int maxHealth;
    public int healthFallRate;

    public Slider thirstSlider;
    public int maxThirst;
    public int thirstFallRate;

    public Slider hungerSlider;
    public int maxHunger;
    public int hungerFallRate;

    public Slider staminaSlider; //NEW
    public int maxStamina; //NEW
    private int staminaFallRate; //NEW
    public int staminaFallMult; //NEW
    private int staminaRegainRate; //NEW
    public int staminaRegainMult; //NEW

    private CharacterController charController; //NEW
    private FirstPersonController playerController; //NEW

    void Start()
    {
        charController = GetComponent<CharacterController>(); //NEW
        playerController = GetComponent<FirstPersonController>(); //NEW

        healthSlider.maxValue = maxHealth;
        healthSlider.value = maxHealth;

        thirstSlider.maxValue = maxThirst;
        thirstSlider.value = maxThirst;

        hungerSlider.maxValue = maxHunger;
        hungerSlider.value = maxHunger;

        staminaSlider.maxValue = maxStamina; //NEW
        staminaSlider.value = maxStamina; //NEW

        staminaFallRate = 1; //NEW
        staminaRegainRate = 1; //NEW
    }

    void Update()
    {
        //HEALTH CONTROLLER
        if (hungerSlider.value <= 0 && (thirstSlider.value <= 0))
        {
            healthSlider.value -= Time.deltaTime / healthFallRate * 2;
        }

        else if(hungerSlider.value <= 0 || thirstSlider.value <= 0)
        {
            healthSlider.value -= Time.deltaTime / healthFallRate;
        }

        if (healthSlider.value <= 0)
        {
            CharacterDeath();
        }

        //HUNGER CONTROLLER
        if (hungerSlider.value >= 0)
        {
            hungerSlider.value -= Time.deltaTime / hungerFallRate;
        }

        else if (hungerSlider.value <= 0)
        {
            hungerSlider.value = 0;
        }

        else if (hungerSlider.value >= maxHunger)
        {
            hungerSlider.value = maxHunger;
        }

        //THIRST CONTROLLER
        if (thirstSlider.value >= 0)
        {
            thirstSlider.value -= Time.deltaTime / thirstFallRate;
        }

        else if (thirstSlider.value <= 0)
        {
            thirstSlider.value = 0;
        }

        else if (thirstSlider.value >= maxThirst)
        {
            thirstSlider.value = maxThirst;
        }

        //STAMINA CONTROL SECTION

        if (charController.velocity.magnitude > 0 && Input.GetKey(KeyCode.LeftShift))
        {
            staminaSlider.value -= Time.deltaTime / staminaFallRate * staminaFallMult;
        }

        else
        {
            staminaSlider.value += Time.deltaTime / staminaRegainRate * staminaRegainMult;
        }

        if (staminaSlider.value >= maxStamina)
        {
            staminaSlider.value = maxStamina;
        }

        else if (staminaSlider.value <= 0)
        {
            staminaSlider.value = 0;
            playerController.m_RunSpeed = playerController.m_WalkSpeed;
        }

        else if (staminaSlider.value >= 0)
        {
            playerController.m_RunSpeed = playerController.m_RunSpeedNorm;
        }
    }

    void CharacterDeath()
    {
        //DO SOMETHING HERE!
    }
}
