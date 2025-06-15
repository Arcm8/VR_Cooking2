using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Management;

public class Starter : MonoBehaviour
{
    IEnumerator Start()
    {
        if (XRGeneralSettings.Instance == null)
        {
            Debug.LogError("? XRGeneralSettings.Instance is NULL!");
            yield break;
        }

        if (XRGeneralSettings.Instance.Manager == null)
        {
            Debug.LogError("? XRGeneralSettings.Instance.Manager is NULL!");
            yield break;
        }

        Debug.Log("¢º XR: Initializing...");
        yield return XRGeneralSettings.Instance.Manager.InitializeLoader();

        if (XRGeneralSettings.Instance.Manager.activeLoader == null)
        {
            Debug.LogError("? XR Loader failed to initialize.");
            yield break;
        }

        XRGeneralSettings.Instance.Manager.StartSubsystems();
        Debug.Log("? XR: Subsystems started. HMD Active: " + UnityEngine.XR.XRSettings.isDeviceActive);
    }
}
