using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonfire1 : MonoBehaviour
{
    /// <summary>
    /// Kada mini boss umre stvori se bonfire, a kada udjemo u radius enejbelat če gui("I") indikator 
    /// i enejbelat če da možemo stisnuti I ako smo u raidusu
    /// </summary>
    public GameObject skeletonBoss1;
    public GameObject bonfire1;
    public GameObject LvlUpMenu;
    public GameObject indicator;
    public bool collisionDetected = false;

    void Update()
    {
        // Check if object1 is null (destroyed or not existent)
        if (skeletonBoss1 == null)
        {
            // Activate object2
            if (bonfire1 != null) // Za svaki slucaj
            {
                bonfire1.SetActive(true);
            }
            else
            {
                Debug.LogWarning("skeletonBoss1 is not assigned!");
            }
        }
    }

    // This method is called when a collision occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the colliding object is GameObject 2
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogError("Player detected in lvl UP area");
            collisionDetected = true;
            LvlUpMenu.SetActive(true);
            indicator.SetActive(true);
        }
    }
    // This method is called when a collision ends
    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the colliding object is GameObject 2
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.LogError("Player left lvl Up area");
            collisionDetected = false;
            LvlUpMenu.SetActive(false);
            indicator.SetActive(false);
        }
    }
}