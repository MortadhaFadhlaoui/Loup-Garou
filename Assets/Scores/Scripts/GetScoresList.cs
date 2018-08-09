using GameSparks.Api.Requests;
using GameSparks.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetScoresList : MonoBehaviour {

    List<String> stringList = new List<String>();
    public GameObject ContentPanel;
    public GameObject ScoreItemPrefab;
    string userId;

    // Use this for initialization
    void Start () {
        getCurrentUser();
        //GameSManager.GetAchievements();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void getCurrentUser()
    {
        new GameSparks.Api.Requests.AccountDetailsRequest().Send((resp) =>
        {
            userId = resp.UserId;
            Debug.Log(userId);
            GetAllScores();
        });
    }
    public void GetAllScores()
    {
        new LeaderboardDataRequest()
        .SetLeaderboardShortCode("SCORE_LEADERBOARD_BEST")
        .SetEntryCount(50)
        .Send((response) => {
            string leaderboardShortCode = response.LeaderboardShortCode;
            GSData scriptData = response.ScriptData;
            Debug.Log("leaderboardShortCode  " + leaderboardShortCode);
            int i= 1;
            foreach (var entry in response.Data)
            {
               
                Debug.Log("USERNAME " + entry.UserName + " SCORE " + entry.GetNumberValue("SCORE"));
                
                GameObject newObj = Instantiate(ScoreItemPrefab) as GameObject;
                ScoreItemController controller = newObj.GetComponent<ScoreItemController>();
               
                controller.Name.text = "Player " + i + " - " +entry.GetNumberValue("SCORE")+"";
               // newObj.transform.SetParent(ContentPanel.transform);
                newObj.transform.localScale = -Vector3.one;
                //newObj.transform.Translate(new Vector3(newObj.transform.position.x, newObj.transform.position.y, -1));
                //newObj.transform.Rotate(new Vector3(180.0f,180.0f,0.0f));
                if (entry.UserId == userId)
                {
                    controller.Name.color = Color.green;
                }
                newObj.transform.position = new Vector3(0, 0, 0);
                newObj.transform.rotation = Quaternion.Euler(180, 180, 0);
                newObj.transform.SetParent(ContentPanel.transform, false);
                newObj.transform.localScale = -Vector3.one;
                i++;
            }
        });
    }
}
