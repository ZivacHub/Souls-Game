using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    public GameObject attackAreas; // Reference to the AttackAreas GameObject
    public CharacterMovement characterMovement;

    private bool attackLocked = false;
    public float attackCooldown = 0.5f;
    private float lastAttackTime = 0f;
    public int staminaCost = 20; // Stamina cost for attacking

    void Start()
    {
        // Ensure AttackAreas GameObject is deactivated initially
        attackAreas.SetActive(false);
    }

    void Update()
    {
        // If attack is not locked and cooldown is over, check for attack input
        if (!attackLocked && Time.time - lastAttackTime > attackCooldown && Input.GetMouseButtonDown(0))
        {
            // Check if there's enough stamina for attacking
            PlayerHPandST playerHPandST = GetComponent<PlayerHPandST>();
            if (playerHPandST != null && playerHPandST.stamina >= staminaCost)
            {
                // Deduct stamina for attacking
                playerHPandST.stamina -= staminaCost;

                Attack();
                // Lock the attack button
                attackLocked = true;
                // Record the time of the attack
                lastAttackTime = Time.time;
            }
        }

        // If attack is locked and cooldown has passed, unlock attack
        if (attackLocked && Time.time - lastAttackTime > attackCooldown)
        {
            attackLocked = false;
        }
    }


    private void Attack()
    {
        PlayerHPandST playerHPandST = GetComponentInParent<PlayerHPandST>(); 
        playerHPandST.PlayerAction();
        // Activate attack area
        attackAreas.SetActive(true);
        // Deactivate attack area after a short delay
        StartCoroutine(DeactivateAttackArea());
    }

    IEnumerator DeactivateAttackArea()
    {
        // Wait for a short delay
        yield return new WaitForSeconds(0.01f);
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

    // Check if player is currently attacking
    public bool IsAttacking()
    {
        return attackLocked;
    }
}
