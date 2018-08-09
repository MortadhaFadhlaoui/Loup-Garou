using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cardanimation : MonoBehaviour {
    public float speed = 0.6f;
    public float angle = 3;
    public GameObject fire;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       // transform.Rotate(new Vector3(0, speed, 0));
        transform.RotateAround(fire.transform.position, Vector3.up, angle);

    }
}
