using System.Collections;
using UnityEngine;

public class Indicator : MonoBehaviour
{
    public GameObject detectionIndicator;
    public bool collisionDetected = false;

    void Start()
    {
        detectionIndicator.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player detected in detection area");
            collisionDetected = true;
            StartCoroutine(Detection());
        }
    }

    IEnumerator Detection()
    {
        detectionIndicator.SetActive(true);
        yield return new WaitForSeconds(3.5f);
        detectionIndicator.SetActive(false);
        Destroy(detectionIndicator);
    }
}
