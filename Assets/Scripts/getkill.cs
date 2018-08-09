using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class getkill : MonoBehaviour {

    List<String> stringList = new List<String>();
    public GameObject ContentPanel;
    public GameObject PlayerItemPrefab;
    private List<String> ListPlayers = new List<String>();

    // Use this for initialization
    void Start () {

        
            GameObject newObj = Instantiate(PlayerItemPrefab) as GameObject;
            killsc controller = newObj.GetComponent<killsc>();
            controller.Name.text = "Chek to kill";
            newObj.transform.localScale = -Vector3.one;
            newObj.transform.position = new Vector3(0, 0, 0);
            newObj.transform.rotation = Quaternion.Euler(180, 180, 0);
            newObj.transform.SetParent(ContentPanel.transform, false);
            newObj.transform.localScale = -Vector3.one;

        
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
