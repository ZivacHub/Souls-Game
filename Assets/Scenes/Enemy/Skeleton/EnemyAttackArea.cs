using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttackArea : MonoBehaviour
{
    private int baseDamage = 3; // Base damage without any weapon

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.GetComponent<PlayerHPandST>() != null)
        {
            PlayerHPandST health = collider.GetComponent<PlayerHPandST>();

            // Calculate total damage based on the equipped weapon's stats
            int totalDamage = baseDamage;

            // Get the equipped weapon stats from the PlayerEquipment script
            EnemyEquipment enemyEquipment = FindObjectOfType<EnemyEquipment>();
            PlayerStats playerStats = FindAnyObjectByType<PlayerStats>();
            EnemyStats enemyStats = FindAnyObjectByType<EnemyStats>();
            if (enemyEquipment != null)
            {
                EnemyEquipment.Weapon equippedWeapon = enemyEquipment.equippedWeapon;

                if (equippedWeapon.physicalAttackPower > 0)
                {
                    totalDamage += equippedWeapon.physicalAttackPower + enemyStats.physicalAttackPower -  playerStats.physicalDefense;
                }

                if (equippedWeapon.magicalAttackPower > 0)
                {
                    totalDamage += equippedWeapon.magicalAttackPower + enemyStats.magicalAttackPower -  playerStats.magicDefense;
                }

                if (equippedWeapon.fireAttackPower > 0)
                {
                    totalDamage += equippedWeapon.fireAttackPower + enemyStats.fireAttackPower -  playerStats.fireDefense;
                }

                if (equippedWeapon.lightningAttackPower > 0)
                {
                    totalDamage += equippedWeapon.lightningAttackPower + enemyStats.lightningAttackPower -  playerStats.lightningDefense;
                }

                if (equippedWeapon.darkAttackPower > 0)
                {
                    totalDamage += equippedWeapon.darkAttackPower + enemyStats.darkAttackPower -  playerStats.darkDefense;
                }
            }

            // Deal damage to the health component
            health.Damage(totalDamage);
        }
    }
}
