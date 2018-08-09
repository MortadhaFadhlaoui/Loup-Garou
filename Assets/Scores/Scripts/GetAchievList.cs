using GameSparks.Api.Requests;
using GameSparks.Core;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetAchievList : MonoBehaviour {

    List<String> stringList = new List<String>();
    public GameObject ContentPanel;
    public GameObject AchievItemPrefab;
    public Sprite firstAchiv;
    public Sprite SecondAchiv;
    public Sprite thirdAchiv;
    public Sprite fourthAchiv;
    Sprite[] Images = new Sprite[5];
   // public Image SecondAchiv;


    // Use this for initialization
    void Start () {
        GetAchievements();
        Images[1] = firstAchiv;
        Images[2] = SecondAchiv;
        Images[3] = thirdAchiv;
        Images[4] = fourthAchiv;

        //GameSparkManager.instance.GetAchievements();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GetAchievements()
    {
        int i = 0;
        new GameSparks.Api.Requests.AccountDetailsRequest().Send((response) => {
            if (!response.HasErrors)
            {
                List<string> achievementsList = response.Achievements;
                achievementsList.Reverse();
                foreach (string s in achievementsList)
                {
                    i++;
                    Debug.Log("Achievement Earned: " + s);
                   // AndroidNativeFunctions.ShowToast("Achievement Earned: " + s);

                    GameObject newObj = Instantiate(AchievItemPrefab) as GameObject;
                    AchievItemController controller = newObj.GetComponent<AchievItemController>();

                    controller.Name.text =  s;
                    controller.ImageAchi.sprite = Images[i];
                    // newObj.transform.SetParent(ContentPanel.transform);
                    newObj.transform.localScale = -Vector3.one;
                    //newObj.transform.Translate(new Vector3(newObj.transform.position.x, newObj.transform.position.y, -1));
                    //newObj.transform.Rotate(new Vector3(180.0f,180.0f,0.0f));

                    newObj.transform.position = new Vector3(0, 0, 0);
                    newObj.transform.rotation = Quaternion.Euler(180, 180, 0);
                    newObj.transform.SetParent(ContentPanel.transform, false);
                    newObj.transform.localScale = -Vector3.one;
                }
            }
            else
            {
                Debug.Log("Error Retrieving Account Details...");
            }
        });
    }
}
