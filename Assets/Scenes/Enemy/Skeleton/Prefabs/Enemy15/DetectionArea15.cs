using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea15: MonoBehaviour
{
    public bool collisionDetected15 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision15)
    {
        // Check if the colliding object is GameObject 2
        if (collision15.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected15 = true;
        }
    }
}
