using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform playerTransform;
    float y = 2.1f;
    // Update is called once per frame
    void Update()
    {
        if (playerTransform != null)
        {
            transform.position = playerTransform.position + new Vector3(0,y,0);
        }
    }

    public void setTarget(Transform target)
    {
        playerTransform = target;
    }

   
}
