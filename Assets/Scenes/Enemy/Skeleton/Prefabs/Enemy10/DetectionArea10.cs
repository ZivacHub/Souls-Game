using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea10: MonoBehaviour
{
    public bool collisionDetected10 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision10)
    {
        // Check if the colliding object is GameObject 2
        if (collision10.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected10 = true;
        }
    }
}
