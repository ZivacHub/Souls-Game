using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea2: MonoBehaviour
{
    public bool collisionDetected2 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision2)
    {
        // Check if the colliding object is GameObject 2
        if (collision2.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected2 = true;
        }
    }
}
