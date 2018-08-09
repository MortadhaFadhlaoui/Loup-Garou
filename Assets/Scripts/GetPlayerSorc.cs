using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetPlayerSorc : MonoBehaviour {

    List<String> stringList = new List<String>();
    public GameObject ContentPanel;
    public GameObject PlayerItemPrefab;
    private List<String> ListPlayers = new List<String>();

    // Use this for initialization
    void Start () {

        foreach (var player in PhotonNetwork.playerList)
        {
            ListPlayers.Add(player.ID+"");
        }
       // stringList.Add("Player 1"); stringList.Add("Player 2"); stringList.Add("Player 3");
        foreach (var entry in PhotonNetwork.playerList)
        {
            GameObject newObj = Instantiate(PlayerItemPrefab) as GameObject;
            soscript controller = newObj.GetComponent<soscript>();
            controller.Name.text = entry.NickName;
            controller.Name.name = entry.ID.ToString();
            newObj.transform.localScale = -Vector3.one;
            newObj.transform.position = new Vector3(0, 0, 0);
            newObj.transform.rotation = Quaternion.Euler(180, 180, 0);
            newObj.transform.SetParent(ContentPanel.transform, false);
            newObj.transform.localScale = -Vector3.one;

        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
