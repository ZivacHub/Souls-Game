using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea5: MonoBehaviour
{
    public bool collisionDetected5 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision5)
    {
        // Check if the colliding object is GameObject 2
        if (collision5.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected5 = true;
        }
    }
}
