using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea14: MonoBehaviour
{
    public bool collisionDetected14 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision14)
    {
        // Check if the colliding object is GameObject 2
        if (collision14.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected14 = true;
        }
    }
}
