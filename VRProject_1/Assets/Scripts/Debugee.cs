using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Debugee : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Is Device Present: " + XRSettings.isDeviceActive);
        Debug.Log("Loaded Device: " + XRSettings.loadedDeviceName);
    }
}
