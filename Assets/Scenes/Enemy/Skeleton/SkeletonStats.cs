using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    [Header("Enemy")]
    public string enemyName = "Skeleton";
    public int maxHealth = 300;
    public int health;
    public int stamina = 0;
    public int mana = 0;
    public int carryWeight = 0;
    public int soulsOnDeath = 50; // Souls to add to the player on death

    [Header("Attributes")]
    public int vigor = 10;
    public int attunement = 0;
    public int endurance = 0;
    public int vitality = 15;
    public int strength = 12;
    public int dexterity = 12;
    public int intelligence = 0;
    public int faith = 0;
    public int luck = 0;

    [Header("Base Power")]
    public int physicalAttackPower = 65;
    public int magicalAttackPower = 0;
    public int fireAttackPower = 0;
    public int lightningAttackPower = 0;
    public int darkAttackPower = 0;
    public int criticalDamage = 100;

    [Header("Defense")]
    public int physicalDefense = 80;
    public int magicDefense = 30;
    public int fireDefense = 30;
    public int lightningDefense = 30;
    public int darkDefense = 30;

    [Header("Resistances")]
    public int bleedResistance = 80;
    public int poisonResistance = 30;
    public int frostbiteResistance = 30;
    public int curseResistance = 30;

    void Start()
    {
    }

    void Update()
    {
        if (health <= 0)
        {
            Debug.Log("Skeleton died");
            Destroy(gameObject);

            // Add souls to the player
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                PlayerStats playerStats = player.GetComponent<PlayerStats>();
                if (playerStats != null)
                {
                    playerStats.AddSouls(soulsOnDeath);
                }
            }
        }
    }
}
