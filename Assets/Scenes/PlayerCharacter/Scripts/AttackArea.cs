using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackArea : MonoBehaviour
{
    private int baseDamage = 3; // Base damage without any weapon

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<Health>() != null)
        {
            Health health = collider.GetComponent<Health>();

            // Calculate total damage based on the equipped weapon's stats
            int totalDamage = baseDamage;

            // Get the equipped weapon stats from the PlayerEquipment script
            PlayerEquipment playerEquipment = FindObjectOfType<PlayerEquipment>();
            PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
            EnemyStats enemyStats = FindAnyObjectByType<EnemyStats>();
            if (playerEquipment != null)
            {
                PlayerEquipment.Weapon equippedWeapon = playerEquipment.equippedWeapon;

                if (equippedWeapon.physicalAttackPower > 0)
                {
                    totalDamage += equippedWeapon.physicalAttackPower + playerStats.physicalAttackPower -  enemyStats.physicalDefense;
                }

                if (equippedWeapon.magicalAttackPower > 0)
                {
                    totalDamage += equippedWeapon.magicalAttackPower + playerStats.magicalAttackPower -  enemyStats.magicDefense;
                }

                if (equippedWeapon.fireAttackPower > 0)
                {
                    totalDamage += equippedWeapon.fireAttackPower + playerStats.fireAttackPower -  enemyStats.fireDefense;
                }

                if (equippedWeapon.lightningAttackPower > 0)
                {
                    totalDamage += equippedWeapon.lightningAttackPower + playerStats.lightningAttackPower -  enemyStats.lightningDefense;
                }

                if (equippedWeapon.darkAttackPower > 0)
                {
                    totalDamage += equippedWeapon.darkAttackPower + playerStats.darkAttackPower -  enemyStats.darkDefense;
                }
            }

            // Deal damage to the health component
            health.Damage(totalDamage);
        }
    }
}
