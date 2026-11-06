using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea4: MonoBehaviour
{
    public bool collisionDetected4 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision4)
    {
        // Check if the colliding object is GameObject 2
        if (collision4.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected4 = true;
        }
    }
}
