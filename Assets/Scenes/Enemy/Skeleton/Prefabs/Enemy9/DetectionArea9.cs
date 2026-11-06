using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea9: MonoBehaviour
{
    public bool collisionDetected9 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision9)
    {
        // Check if the colliding object is GameObject 2
        if (collision9.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected9 = true;
        }
    }
}
