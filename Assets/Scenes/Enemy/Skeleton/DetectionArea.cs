using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea: MonoBehaviour
{
    public bool collisionDetected = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is GameObject 2
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected = true;
        }
    }
}
