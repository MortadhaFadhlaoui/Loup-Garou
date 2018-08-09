using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class checksolde : MonoBehaviour {

	// Use this for initialization
	void Start () {
        PlayerPrefs.SetInt("solde", 1000);
        PlayerPrefs.SetInt("c1", 0);
        PlayerPrefs.SetInt("c2", 0);
    }

    // Update is called once per frame
    void Update () {
     
        int f = PlayerPrefs.GetInt("solde");
        this.GetComponent<Text>().text = f.ToString();
    }

}
