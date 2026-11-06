using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea16: MonoBehaviour
{
    public bool collisionDetected16 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision16)
    {
        // Check if the colliding object is GameObject 2
        if (collision16.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected16 = true;
        }
    }
}
