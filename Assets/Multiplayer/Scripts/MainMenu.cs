using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Networking.Match;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour {
    public InputField matchName;
    // Use this for initialization
    void Start () {
        NetworkManager.singleton.StartMatchMaker();
    }
    //call this method to request a match to be created on the server
    public void CreateInternetMatch()
    {
        NetworkManager.singleton.matchMaker.CreateMatch(matchName.text, 4, true, "", "", "", 0, 0, OnInternetMatchCreate);
    }

    //this method is called when your request for creating a match is returned
    private void OnInternetMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
    {
        if (success)
        {
            //Debug.Log("Create match succeeded");

            MatchInfo hostInfo = matchInfo;
            NetworkServer.Listen(hostInfo, 9000);

            NetworkManager.singleton.StartHost(hostInfo);
        }
        else
        {
            Debug.LogError("Create match failed");
        }
    }
  
    void Update () {
		
	}
}
