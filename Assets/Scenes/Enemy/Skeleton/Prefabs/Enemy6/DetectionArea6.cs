using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DetectionArea6: MonoBehaviour
{
    public bool collisionDetected6 = false;
    
    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision6)
    {
        // Check if the colliding object is GameObject 2
        if (collision6.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected6 = true;
        }
    }
}
