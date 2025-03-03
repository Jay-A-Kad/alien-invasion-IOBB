using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraSwitch : MonoBehaviour
{
    public Camera FPV;
    public Camera TPV;
    private bool isCameraActive = true;
    void Start()
    {
        TPV.gameObject.SetActive(true);
        FPV.gameObject.SetActive(false);
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            isCameraActive = !isCameraActive;
            FPV.gameObject.SetActive(isCameraActive);
            TPV.gameObject.SetActive(!isCameraActive);
        }
    }
}