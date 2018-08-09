using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class testkill : MonoBehaviour {

	// Use this for initialization
	void Start () {
		if(PlayerPrefs.GetInt("kill") == 1)
        {
            AndroidNativeFunctions.ShowToast("You Lost ! Please remove your headset");
            PlayerPrefs.SetInt("kill", 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
