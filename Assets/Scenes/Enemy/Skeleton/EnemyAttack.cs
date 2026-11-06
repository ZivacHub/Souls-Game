using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{   
    public DetectionAttack detectionAttack; // Reference to DetectionAttack script
    public GameObject attackAreas; // Reference to the AttackAreas GameObject
    public GameObject attackAreaObject; // Needed to determine how long player stands in it to attack
    private bool attackLocked = false;
    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;

    void Start()
    {
        // Ensure AttackAreas GameObject is deactivated initially
        attackAreas.SetActive(false);
    }

    void Update()
    {
        // If attack is not locked and cooldown is over, check for attack possablity
        if (!attackLocked && Time.time - lastAttackTime > attackCooldown && detectionAttack.canAttack)
        {
            Attack();
            // Lock the attack button
            attackLocked = true;
            // Record the time of the attack
            lastAttackTime = Time.time;
        }

        // If attack is locked and cooldown has passed, unlock attack
        if (attackLocked && Time.time - lastAttackTime > attackCooldown)
        {
            attackLocked = false;
        }
    }

    private void Attack()
    {
        // Activate attack area
        attackAreas.SetActive(true);
        // Deactivate attack area after a short delay
        StartCoroutine(DeactivateAttackArea());
        Debug.Log("Enemy Attacked!");
    }

    IEnumerator DeactivateAttackArea()
    {
        // Wait for a short delay
        yield return new WaitForSeconds(0.009f);
        // Deactivate attack area
        attackAreas.SetActive(false);
    }

    // Set the attack area active based on direction
    public void SetAttackDirection(int direction)
    {
        if (direction >= 0 && direction < attackAreas.transform.childCount)
        {
            // Deactivate all attack areas
            for (int i = 0; i < attackAreas.transform.childCount; i++)
            {
                attackAreas.transform.GetChild(i).gameObject.SetActive(false);
            }

            // Activate the attack area for the specified direction
            attackAreas.transform.GetChild(direction).gameObject.SetActive(true);
        }
    }

    // Check if enemy is currently attacking
    public bool IsAttacking()
    {
        return attackLocked;
    }
}
