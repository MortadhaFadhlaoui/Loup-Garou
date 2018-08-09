using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class NetworkManger_Camera : NetworkManager {
    [Header("Scene camera Properties")]
    [SerializeField] Transform sceneCamera;
    [SerializeField] float CameraRotationRaduis = 24f;
    [SerializeField] float CameraRotationSpeed = 3f;
    [SerializeField] bool canRotate = true;
    float rotaion;
    // Use this for initialization
    public override void OnStartClient(NetworkClient client)
    {
        canRotate = false;
    }
    public override void OnStartHost()
    {
        canRotate = false;
    }
    public override void OnStopHost()
    {
        canRotate = true;
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (!canRotate)
            return;
        rotaion += CameraRotationSpeed * Time.deltaTime;
        if (rotaion >= 360f)
            rotaion -= 360f;
        sceneCamera.position = Vector3.zero;
        sceneCamera.rotation = Quaternion.Euler(0f, rotaion, 0f);
        sceneCamera.Translate(0f, CameraRotationRaduis, -CameraRotationRaduis);
        sceneCamera.LookAt(Vector3.zero);

    }
}
