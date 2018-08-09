using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {


    private PhotonView PhotonView;


	// Use this for initialization
	void Start () {       
	}

    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
    }

    // Update is called once per frame
    void Update () {
        if(PhotonView.isMine)
        CheckInput();

    }
    private void CheckInput()
    {
        float moveSpeed = 100f;
        float rotateSpeed = 500f;

        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal");

        transform.position += transform.forward * (vertical * moveSpeed * Time.deltaTime);
        transform.Rotate(new Vector3(0, horizontal * rotateSpeed * Time.deltaTime, 0));
    }
}
