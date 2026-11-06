using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea17: MonoBehaviour
{
    public bool collisionDetected17 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision17)
    {
        // Check if the colliding object is GameObject 2
        if (collision17.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected17 = true;
        }
    }
}
