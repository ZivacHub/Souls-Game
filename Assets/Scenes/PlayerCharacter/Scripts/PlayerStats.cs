using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    [Header("Hero")]
    public int level = 1;
    public int souls = 0;
    public int health;
    public int maxHealth = 570;
    public int stamina;
    public int maxStamina = 160;
    public int mana = 90;
    public int carryWeight = 70;

    [Header("Attributes")]
    public int vigor = 12;
    public int attunement = 10;
    public int endurance = 11;
    public int vitality = 15;
    public int strength = 13;
    public int dexterity = 12;
    public int intelligence = 9;
    public int faith = 9;
    public int luck = 7;

    [Header("Base Power")]
    public int physicalAttackPower = 100;     // Placeholder value
    public int magicalAttackPower = 50;       // Placeholder value
    public int fireAttackPower = 50;          // Placeholder value
    public int lightningAttackPower = 50;     // Placeholder value
    public int darkAttackPower = 50;          // Placeholder value
    public int criticalDamage = 150;          // Placeholder value

    [Header("Defense")]
    public int physicalDefense = 100;         // Placeholder value
    public int magicDefense = 50;             // Placeholder value
    public int fireDefense = 50;              // Placeholder value
    public int lightningDefense = 50;         // Placeholder value
    public int darkDefense = 50;              // Placeholder value

    [Header("Resistances")]
    public int bleedResistance = 100;         // Placeholder value
    public int poisonResistance = 50;         // Placeholder value
    public int frostbiteResistance = 50;      // Placeholder value
    public int curseResistance = 50;          // Placeholder value

    void Start()
    {
        // Set initial health to max health
        health = maxHealth;
    }

    void Update()
    {
        // Check if health is less than or equal to 0
        if (health <= 0)
        {
            Debug.Log("Player died");
            Destroy(gameObject); // Destroy the game object
        }
    }

    // Method to add souls
    public void AddSouls(int soulsToAdd)
    {
        souls += soulsToAdd;
    }
}
