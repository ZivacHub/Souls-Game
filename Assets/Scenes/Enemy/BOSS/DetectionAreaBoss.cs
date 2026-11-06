using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionAreaBoss: MonoBehaviour
{
    public bool collisionDetectedBoss = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collisionBoss)
    {
        // Check if the colliding object is GameObject 2
        if (collisionBoss.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetectedBoss = true;
        }
    }
}
