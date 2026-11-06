using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea19: MonoBehaviour
{
    public bool collisionDetected19 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision19)
    {
        // Check if the colliding object is GameObject 2
        if (collision19.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected19 = true;
        }
    }
}
