using UnityEngine;

public class EnemyEquipment : MonoBehaviour
{
    // Define a class for the weapon
    [System.Serializable]
    public class Weapon
    {
        public string name;
        public int physicalAttackPower;
        public int magicalAttackPower;
        public int fireAttackPower;
        public int lightningAttackPower;
        public int darkAttackPower;
        public int criticalDamage;
        public int carryWeight;
    }

    // Currently equipped weapon
    public Weapon equippedWeapon;

    void Start()
    {
        // Initialize the starting weapon of the Knight
        equippedWeapon = new Weapon
        {
            name = "Broken Sword",
            physicalAttackPower = 70,
            magicalAttackPower = 0,
            fireAttackPower = 0,
            lightningAttackPower = 0,
            darkAttackPower = 0,
            criticalDamage = 100,
            carryWeight = 10 // Adjust this value as needed
        };
    }

    // Method to equip a weapon
    public void EquipWeapon(Weapon weapon)
    {
        equippedWeapon = weapon;
    }
}
