using UnityEngine;


public class MultiPlayer : MonoBehaviour {
    public GameObject play,leaderboard, options,panel,bond,menuplay,menufindagame,CurrentRoom,Lobby;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void ClickPlay()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable)
        {
            Debug.Log("Error. Check internet connection!");
            AndroidNativeFunctions.ShowToast("Please check your Internet connection!", false);
            //EditorUtility.DisplayDialog("Error", "Check internet connection!", "Ok");
        }
        else
        {
            menuplay.SetActive(true);
            bond.SetActive(true);
            play.SetActive(false);
            panel.SetActive(true);
            leaderboard.SetActive(false);
            options.SetActive(false);
        }
    }
    public void ClickPlayWithFriends()
    {
        menufindagame.SetActive(true);
        menuplay.SetActive(false);
    }

   

    public void ClickBack()
    {
        if (menufindagame.activeSelf)
        {
            menufindagame.SetActive(false);
            menuplay.SetActive(true);
        }
        else if (menuplay.activeSelf)
        {
            menuplay.SetActive(false);
            bond.SetActive(false);
            play.SetActive(true);
            panel.SetActive(false);
            leaderboard.SetActive(true);
            options.SetActive(true);
        }
        else if (Lobby.activeSelf)
        {
            Lobby.gameObject.SetActive(false);
            menuplay.gameObject.SetActive(true);
        }
        
       
    }

}
