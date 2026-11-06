using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea13: MonoBehaviour
{
    public bool collisionDetected13 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision13)
    {
        // Check if the colliding object is GameObject 2
        if (collision13.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected13 = true;
        }
    }
}
