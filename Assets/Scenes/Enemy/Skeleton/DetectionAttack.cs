using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionAttack : MonoBehaviour
{
    private bool playerInArea = false;
    private float enterTime;
    public bool canAttack = false;

    public float detectionAttacktime = 0.5f;

    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is GameObject 2
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea = true;
            enterTime = Time.time; // Record the time when player enters the area
        }
    }

    // This method is called when a collision ends
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object is GameObject 2
        if (collision.gameObject.CompareTag("Player"))
        {
            playerInArea = false;
        }
    }

    // This method is called every frame
    private void Update()
    {
        // If the player is in the area and has been there for at least 1 second
        if (playerInArea && Time.time - enterTime >= detectionAttacktime)
        {
            canAttack = true;
            Debug.Log($"Player Stood in enemy's attack area for {detectionAttacktime} second");
        }
        else
        {
            // If the player exits the attack area, set canAttack to false
            canAttack = false;
        }
    }

}
