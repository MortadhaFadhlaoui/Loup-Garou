using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class RequestPermission : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        RequestMicroPermission();
       // RequestStoragePermission();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Update UP REQUEST");
    }

    void RequestStoragePermission()
    {
        if (!UniAndroidPermission.IsPermitted(AndroidPermission.WRITE_EXTERNAL_STORAGE))
        {
            Debug.Log("WRITE_EXTERNAL_STORAGE NOT Permitted");
            UniAndroidPermission.RequestPermission(AndroidPermission.WRITE_EXTERNAL_STORAGE, null, null, null);
        }
    }

    void RequestMicroPermission()
    {
        if (!UniAndroidPermission.IsPermitted(AndroidPermission.RECORD_AUDIO))
        {
            Debug.Log("RECORD_AUDIO NOT Permitted");
            UniAndroidPermission.RequestPermission(AndroidPermission.RECORD_AUDIO, RequestStoragePermission, null, null);
        }
    }
}

