using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class ToggleVR : MonoBehaviour {

    int vrModeInt;


    // Use this for initialization
    void Start () {
        EnableVR();
    }
	
	// Update is called once per frame
	void Update () {

    }

    IEnumerator LoadDevice(string newDevice, bool enable)
    {
        XRSettings.LoadDeviceByName(newDevice);
        yield return null;
        XRSettings.enabled = enable;
    }

    void EnableVR()
    {
        StartCoroutine(LoadDevice("cardboard", true));
    }

   public void DisableVR()
    {
        StartCoroutine(LoadDevice("", false));
    }
}
