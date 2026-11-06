using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea7: MonoBehaviour
{
    public bool collisionDetected7 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision7)
    {
        // Check if the colliding object is GameObject 2
        if (collision7.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected7 = true;
        }
    }
}
