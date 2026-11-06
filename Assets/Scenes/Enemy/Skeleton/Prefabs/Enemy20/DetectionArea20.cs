using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea20: MonoBehaviour
{
    public bool collisionDetected20 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision20)
    {
        // Check if the colliding object is GameObject 2
        if (collision20.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected20 = true;
        }
    }
}
