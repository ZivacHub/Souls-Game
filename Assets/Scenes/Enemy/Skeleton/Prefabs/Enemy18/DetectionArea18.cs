using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea18: MonoBehaviour
{
    public bool collisionDetected18 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision18)
    {
        // Check if the colliding object is GameObject 2
        if (collision18.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected18 = true;
        }
    }
}
