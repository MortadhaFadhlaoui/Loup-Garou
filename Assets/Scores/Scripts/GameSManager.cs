using GameSparks.Api.Requests;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSManager : MonoBehaviour
{

    public static GameSManager instance = null;
    bool test = false;
    private bool x = true;
    void Awake()
    {
        if (instance == null) // check to see if the instance has a reference
        {
            instance = this; // if not, give it a reference to this class...
            DontDestroyOnLoad(this.gameObject); // and make this object persistent as we load new scenes  

        }
        else // if we already have a reference then remove the extra manager from the scene
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if (GameSparks.Core.GS.Available && !GameSparks.Core.GS.Authenticated)
        {
            AuthUser();
        }/*
        if (GameSparks.Core.GS.Available && GameSparks.Core.GS.Authenticated)
        {
            if(x == true)
            {
                SubmitScore(140);
                x = false;
            }
         
           

        }*/
    }

    public void AuthUser()
    {
        new GameSparks.Api.Requests.DeviceAuthenticationRequest().Send((response) => {
            if (!response.HasErrors)
            {
                Debug.Log("Device Authenticated...");
                GameSparks.Api.Messages.AchievementEarnedMessage.Listener += AchievementMessageHandler;

            }
            else
            {
                Debug.Log("Error Authenticating Device...");
            }
        });
    }

    void AchievementMessageHandler(GameSparks.Api.Messages.AchievementEarnedMessage _message)
    {
        Debug.Log("AWARDED ACHIEVEMENT \n " + _message.AchievementName);
    }

    public void SaveAchievemnt(string achName)
    {
        new LogEventRequest().SetEventKey(achName).Send((response) => {
            Debug.Log(response.JSONString);
            /*  if(!response.HasErrors)
              {
                  Debug.Log("SaveAchievemnt  " + response.ToString());
              }    */
        });
    }

    /*
     public static void GetAchievements()
    {
        new GameSparks.Api.Requests.AccountDetailsRequest().Send((response) => {
        if (!response.HasErrors)
        {
            List<string> achievementsList = response.Achievements;

            foreach (string s in achievementsList)
            {
                Debug.Log("Achievement Earned: " + s);
                AndroidNativeFunctions.ShowToast("Achievement Earned: " + s);
                }
            }
            else
            {
                Debug.Log("Error Retrieving Account Details...");
            }
        });
    }*/

    public void SubmitScore(int scoreValue)
    {

        string attributeValue = scoreValue.ToString();

        new LogEventRequest().
            SetEventKey("SCORE_LEADERBOARD").SetEventAttribute("SCORE", attributeValue).Send((response) => {
                if (!response.HasErrors)
                {
                    Debug.Log("Score posted successfully");
                }
                else
                {
                    Debug.Log("Error posting score");
                }
            });

    }
}
