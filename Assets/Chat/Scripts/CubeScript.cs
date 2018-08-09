using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeScript : MonoBehaviour {


    public Material inside;
    public Material outside;

	// Use this for initialization
	void Start () {
        GetComponent<Renderer>().material = outside;
    }
	
	// Update is called once per frame
	void Update () {
        

    }

    public void insideEvent()
    {
        GetComponent<Renderer>().material = inside;
    }

    public void outsideEvent()
    {
        GetComponent<Renderer>().material = outside;
    }
}
