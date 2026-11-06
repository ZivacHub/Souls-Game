using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea12: MonoBehaviour
{
    public bool collisionDetected12 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision12)
    {
        // Check if the colliding object is GameObject 2
        if (collision12.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected12 = true;
        }
    }
}
