using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VictoryHandler : MonoBehaviour
{
    public GameObject bossObject;
    public GameObject victoryMenu;

    // Update is called once per frame
    void Update()
    {
        if (bossObject == null)
        {
            victoryMenu.SetActive(true);
        }
    }
}
