using UnityEngine;
using UnityEngine.UI;


public class LevelUpMenu : MonoBehaviour
{
    public PlayerStats playerStats;
    public PlayerHPandST playerHpandSt;
    public Text soulsText; // Text element to display souls
    public GameObject levelUpPanel; // Reference to the level up panel GameObject

    public int healthUpgradeCost = 100; // Souls required for health upgrade
    public int attackUpgradeCost = 100; // Souls required for attack upgrade
    public int defenseUpgradeCost = 100; // Souls required for defense upgrade
    public int regenerationUpgradeCost = 500;
    public bool healthRegeneration = false;
    public static bool GameIsPaused = false;

    // Start is called before the first frame update
    void Start()
    {
        // Display initial souls count
        UpdateSoulsText();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {
            Debug.LogError("I pressed");
            if (GameIsPaused)
            {
                Resume();
                UpdateSoulsText();
            } else
            {
                Pause();
            }
        }
    }


    public void Resume ()
    {
        levelUpPanel.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    void Pause ()
    {
        levelUpPanel.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    // Update souls text to display current amount of souls
    void UpdateSoulsText()
    {
        soulsText.text = "Souls: " + playerStats.souls.ToString();
    }

    // Function called when Health button is clicked
    public void UpgradeHealth()
    {
        // Check if the player has enough souls to upgrade health
        if (playerStats.souls >= healthUpgradeCost)
        {
            // Subtract souls cost
            playerStats.souls -= healthUpgradeCost;

            // Increase max health and health by 50
            playerStats.maxHealth += 50;
            playerStats.health += 50;

            // Update souls text after upgrade
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls to upgrade health!");
            // Optionally, display a message to the player indicating insufficient souls
        }
    }

    // Function called when Attack button is clicked
    public void UpgradeAttack()
    {
        // Check if the player has enough souls to upgrade attack
        if (playerStats.souls >= attackUpgradeCost)
        {
            // Subtract souls cost
            playerStats.souls -= attackUpgradeCost;

            // Increase base attack by 20
            playerStats.physicalAttackPower += 20;

            // Update souls text after upgrade
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls to upgrade attack!");
            // Optionally, display a message to the player indicating insufficient souls
        }
    }

    // Function called when Defense button is clicked
    public void UpgradeDefense()
    {
        // Check if the player has enough souls to upgrade defense
        if (playerStats.souls >= defenseUpgradeCost)
        {
            // Subtract souls cost
            playerStats.souls -= defenseUpgradeCost;

            // Increase physical defense by 10
            playerStats.physicalDefense += 10;

            // Update souls text after upgrade
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls to upgrade defense!");
            // Optionally, display a message to the player indicating insufficient souls
        }
    }

    // Function called when Regeneration button is clicked
    public void UpgradeRegeneration()
    {
        // Check if the player has enough souls to upgrade defense
        if (playerStats.souls >= regenerationUpgradeCost)
        {
            // Subtract souls cost
            playerStats.souls -= regenerationUpgradeCost;

            // Set regeneration to true
            healthRegeneration = true;

            // Update souls text after upgrade
            UpdateSoulsText();
        }
        else
        {
            Debug.Log("Not enough souls to upgrade defense!");
            // Optionally, display a message to the player indicating insufficient souls
        }
    }
}
