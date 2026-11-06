using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public float health = 200f;
    private float lerpTimer;
    public float chipSpeed = 2f; //The bigger the value the slower chip is, jer se dijeli dolje
    private int maxHealth; // New variable to store maximum health

    public Image frontHealthBar;
    public Image backHealthBar;
    public Image maxHealthBar;

    private Coroutine fadeInCoroutine; // Coroutine reference for fading in the health bar
    private Coroutine fadeOutCoroutine; // Coroutine reference for fading out the health bar

    private float timeSinceLastDamage; // Time since the last damage was taken
    public float fadeOutDelay = 3f; // Time to wait before fading out the health bar

    public GameObject modelObject;
    public GameObject skeletonAiObjcet;
    public GameObject skeletonObject;

    // Start is called before the first frame update
    void Start()
    {
        // Get the maximum health from the parent EnemyStats script
        EnemyStats enemyStats = GetComponentInParent<EnemyStats>();
        if (enemyStats != null)
        {
            maxHealth = enemyStats.maxHealth;
        }
        else
        {
            Debug.LogError("EnemyStats component not found on parent GameObject.");
        }

        // Set current health to maximum health at the start
        health = maxHealth;

        // Initially hide the health bar
        SetHealthBarVisible(false);
    }

    // Update is called once per frame
    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
        UpdateHealhtUI();
        // Update time since last damage
        timeSinceLastDamage += Time.deltaTime;

        // If no damage has been taken for the specified delay, fade out the health bar
        if (timeSinceLastDamage >= fadeOutDelay && frontHealthBar.gameObject.activeSelf)
        {
            fadeOutCoroutine = StartCoroutine(FadeOutHealthBar(1f));
        }
    }
    public void UpdateHealhtUI(){
        Debug.Log(health);
        float fillF = frontHealthBar.fillAmount;
        float fillB = backHealthBar.fillAmount;
        float hFraction = health / maxHealth;
        if (fillB > hFraction){
            frontHealthBar.fillAmount = hFraction;
            lerpTimer += Time.deltaTime;
            float precentComplete = lerpTimer / chipSpeed;
            backHealthBar.fillAmount = Mathf.Lerp(fillB,hFraction,precentComplete);
        }
    }

    public void Damage(int amount)
    {
        if (amount < 0)
        {
            throw new System.ArgumentOutOfRangeException("Cannot have negative Damage");
        }

        this.health -= amount;

        // Log who took damage and for how much
        Debug.Log($"{GetComponentInParent<EnemyStats>().name} took {amount} damage. Current HP: {health}/{maxHealth}");

        // Update the health bar fill amount
        UpdateHealthBar();

        // Show the health bar and reset the fade out coroutine
        ShowHealthBar();

        // Reset the time since last damage
        timeSinceLastDamage = 0f;

        if (health <= 0)
        {
            Die();
            EnemyStats enemyStats = FindAnyObjectByType<EnemyStats>();
            PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
            playerStats.souls = playerStats.souls + enemyStats.soulsOnDeath;
            Debug.Log($"{GetComponentInParent<EnemyStats>().soulsOnDeath} souls gained");
        }
        lerpTimer = 0f;
    }

    private void Die()
    {
        Debug.Log("Enemy died!");
        Destroy(skeletonObject);
    }

    private void UpdateHealthBar()
    {
        // Calculate the current health percentage
        float healthPercentage = Mathf.Clamp01((float)health / maxHealth);

        // Set the fill amount of the health bar's "FrontHp" image
        if (frontHealthBar != null)
        {
            frontHealthBar.fillAmount = healthPercentage;
        }
        else
        {
            Debug.LogError("FrontHp Image reference is not set in the Health script.");
        }
    }

    private void ShowHealthBar()
    {
        // If the health bar is not already visible, start fading it in
        if (!frontHealthBar.gameObject.activeSelf)
        {
            SetHealthBarVisible(true);
            fadeInCoroutine = StartCoroutine(FadeInHealthBar(1f));
        }
        else // If the health bar is already visible, reset the fade out coroutine
        {
            if (fadeOutCoroutine != null)
            {
                StopCoroutine(fadeOutCoroutine);
            }
        }
    }

    private void SetHealthBarVisible(bool visible)
    {
        if (backHealthBar != null && frontHealthBar != null)
        {
            backHealthBar.gameObject.SetActive(visible);
            frontHealthBar.gameObject.SetActive(visible);
        }
        else
        {
            Debug.LogError("BackHp or FrontHp Image reference is not set in the Health script.");
        }
    }

    private IEnumerator FadeInHealthBar(float duration)
    {
        float currentTime = 0f;
        Color backHpColor = backHealthBar.color;
        Color frontHpColor = frontHealthBar.color;

        while (currentTime < duration)
        {
            float alpha = currentTime / duration;
            backHpColor.a = alpha;
            frontHpColor.a = alpha;

            backHealthBar.color = backHpColor;
            frontHealthBar.color = frontHpColor;

            currentTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the alpha value is exactly 1 after the coroutine finishes
        backHpColor.a = 1f;
        frontHpColor.a = 1f;
        backHealthBar.color = backHpColor;
        frontHealthBar.color = frontHpColor;
    }

    private IEnumerator FadeOutHealthBar(float duration)
    {
        float currentTime = 0f;
        Color backHpColor = backHealthBar.color;
        Color frontHpColor = frontHealthBar.color;

        while (currentTime < duration)
        {
            float alpha = 1f - (currentTime / duration);
            backHpColor.a = alpha;
            frontHpColor.a = alpha;

            backHealthBar.color = backHpColor;
            frontHealthBar.color = frontHpColor;

            currentTime += Time.deltaTime;
            yield return null;
        }

        // Ensure that the alpha value is exactly 0 after the coroutine finishes
        backHpColor.a = 0f;
        frontHpColor.a = 0f;
        backHealthBar.color = backHpColor;
        frontHealthBar.color = frontHpColor;

        // Hide the health bar after fading out
        SetHealthBarVisible(false);
    }
}
