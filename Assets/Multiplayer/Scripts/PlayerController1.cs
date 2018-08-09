using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class PlayerController1 : NetworkBehaviour
{

	// Use this for initialization
	void Start () {
      
    }

    void Update()
    {
        if (!isLocalPlayer)
        {
            Destroy(this);
            return;
        }
        var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
        var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

        transform.Rotate(0, x, 0);
        transform.Translate(0, 0, z);
    }
    public override void OnStartLocalPlayer()
    {
        Debug.Log("OnStartLocalPlayer");
        Camera.main.GetComponent<CameraFollow>().setTarget(gameObject.transform);
    }
}
