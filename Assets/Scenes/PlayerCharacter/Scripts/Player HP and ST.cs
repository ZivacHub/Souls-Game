using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHPandST : MonoBehaviour
{
    // Health variables
    public float health;
    public GameObject deathScreen;
    public Image frontHealthBar;
    public Image backHealthBar;

    // Stamina variables
    public float stamina;
    public Image frontStaminaBar;
    public Image backStaminaBar;

    private float lastActionTime;
    private float lastDamageTime;
    private float lerpTimerHealth;
    private float lerpTimerStamina;
    private int maxHealth;
    private int maxStamina;

    // Health and stamina regeneration parameters
    public float chipSpeed = 30f;
    public float regenerateStaminaDelay = 3f;
    public float staminaRegenRate = 0.05f;
    public float staminaRegenInterval = 0.5f;

/// ////////////////////////////////////////////////////////

    public float healthRegenInterval = 0.5f;
    public float healthRegenRate = 0.1f;
    public float healthRegenDelay = 3f;
    public GameObject regenerationObject;
    private bool regerenation;

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
        health = playerStats.maxHealth;
        maxHealth = playerStats.maxHealth;
        // Initialize health and stamina
        InitHealthAndStamina();

        // Start the stamina and health regeneration coroutines
        StartCoroutine(RegenerateStamina());
        StartCoroutine(RegenerateHealth());
    }

    // Update is called once per frame
    void Update()
    {
        PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
        if (playerStats.maxHealth > maxHealth)
        {
            health = playerStats.health;
            maxHealth = playerStats.maxHealth;
        }
        // Update UI elements for health and stamina
        UpdateHealthUI();
        UpdateStaminaUI();
    }

    // Initialize health and stamina
    void InitHealthAndStamina()
    {
        PlayerStats playerStats = GetComponentInParent<PlayerStats>();
        if (playerStats != null)
        {
            maxHealth = playerStats.maxHealth;
            maxStamina = playerStats.maxStamina;
        }
        else
        {
            Debug.LogError("PlayerStats component not found on parent GameObject.");
        }

        health = maxHealth;
        stamina = maxStamina;
        lastActionTime = Time.time;
        lastDamageTime = Time.time;
    }

    // Update health UI elements
    void UpdateHealthUI()
    {
        float hFraction =  Mathf.Clamp01(health / maxHealth);
        if (backHealthBar.fillAmount > hFraction)
        {
            frontHealthBar.fillAmount = hFraction;
            lerpTimerHealth += Time.deltaTime;
            float precentHealthComplete = lerpTimerHealth / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(backHealthBar.fillAmount, hFraction, precentHealthComplete);
        }

        frontHealthBar.fillAmount = hFraction;
        if (frontHealthBar.fillAmount == 1f)
        {
            backHealthBar.fillAmount = 1f;
        }
    }

    // Update stamina UI elements
    void UpdateStaminaUI()
    {
        float staminaPercentage = Mathf.Clamp01(stamina / maxStamina);

        if (backStaminaBar.fillAmount > staminaPercentage)
        {
            lerpTimerStamina += Time.deltaTime;
            float precentStaminaComplete = lerpTimerStamina / chipSpeed;
            backStaminaBar.fillAmount = Mathf.Lerp(backStaminaBar.fillAmount, staminaPercentage, precentStaminaComplete);
        }

        frontStaminaBar.fillAmount = staminaPercentage;
    }

    // Damage the player's health
    public void Damage(int amount)
    {
        regenerationObject.SetActive(false);

        lastDamageTime = Time.time;
        CharacterMovement characterMovement = FindObjectOfType<CharacterMovement>();
        if (!characterMovement.isDashing)
        {
            health -= amount;
            UpdateHealthUI();

            if (health <= 0)
            {
                Die();
            }

            lerpTimerHealth = 0f;
        }
        else
        {
            //Canot Take damage since we are dashing
        }
    }

    // Die function
    void Die()
    {
        deathScreen.SetActive(true);
    }

    // Regenerate stamina over time
    IEnumerator RegenerateStamina()
    {
        while (true)
        {
            yield return new WaitForSeconds(staminaRegenInterval);

            if (Time.time - lastActionTime > regenerateStaminaDelay)
            {
                stamina = Mathf.Min(stamina + maxStamina * staminaRegenRate, maxStamina);
                backStaminaBar.fillAmount = frontStaminaBar.fillAmount;
                lerpTimerStamina = 0f;
            }
        }
    }

    // Regenerate health over time if not damaged for a certain period
    IEnumerator RegenerateHealth()
    {
        while (true)
        {
            yield return new WaitForSeconds(healthRegenInterval);

            LevelUpMenu levelUpmenu = FindObjectOfType<LevelUpMenu>();
            if (levelUpmenu != null && levelUpmenu.healthRegeneration)
            {
                Debug.LogError("HEALTH REGERENATION ACTIVE");
                // Check if enough time has passed since the last action
                if (Time.time - lastDamageTime > healthRegenDelay)
                {
                    // Check if the player's health is below max health
                    if (health < maxHealth)
                    {
                        // Regenerate health
                        health = Mathf.Min(health + maxHealth * healthRegenRate, maxHealth);
                        backHealthBar.fillAmount = frontHealthBar.fillAmount;
                        lerpTimerHealth = 0f;

                        // Activate regeneration object
                        if (regenerationObject != null)
                        {
                            regenerationObject.SetActive(true);
                        }
                    }
                }
            }
            else
            {
                // Deactivate regeneration object if health regeneration is not enabled
                if (regenerationObject != null)
                {
                    regenerationObject.SetActive(false);
                }
            }
            yield return null;
        }
    }


    // Call this function whenever the player performs an action (attack, dash)
    public void PlayerAction()
    {
        lastActionTime = Time.time;
        lerpTimerStamina = 0f;
    }
}
