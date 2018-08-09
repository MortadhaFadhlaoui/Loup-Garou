using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPointer : MonoBehaviour {

	// Use this for initialization
	void Start () {
        Debug.Log("PlayerPointer START ");
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnFocusIn()
    {
        Debug.Log("OnFocusIn VOTE TO KILL ");
    }

    public void OnFocusOut()
    {
        Debug.Log("OnFocusOut");
    }
}
