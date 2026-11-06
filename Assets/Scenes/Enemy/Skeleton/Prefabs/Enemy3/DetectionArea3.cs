using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea3: MonoBehaviour
{
    public bool collisionDetected3 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision3)
    {
        // Check if the colliding object is GameObject 2
        if (collision3.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected3 = true;
        }
    }
}
