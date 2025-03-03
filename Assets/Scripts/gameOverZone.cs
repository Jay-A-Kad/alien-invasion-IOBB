using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gameOverZone : MonoBehaviour
{
    public GameObject uiCanvas;

    void Start()
    {

        if (uiCanvas != null)
            uiCanvas.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (uiCanvas != null)
                uiCanvas.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            if (uiCanvas != null)
                uiCanvas.SetActive(false);
        }
    }
}
