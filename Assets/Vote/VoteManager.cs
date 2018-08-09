using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VoteManager : MonoBehaviour
{
    public bool isVoted = false;

    public bool isVotedJour = false;

    public static VoteManager instance;

    private List<String> ListPlayers = new List<String>();
 
   

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }
    }

    void Start()
    {

       

        foreach (var player in PhotonNetwork.playerList)
        {
            ListPlayers.Add(player.ID + "");
        }
        foreach (String p in ListPlayers)
        {
            PlayerNetwork.Instance.PlayerVotesMap[p] = 0;
        }
    }

    // Update is called once per frame
    void Update()
    {
      
        
        
    }

    public void AddVote(String name)
    {
        isVoted = true;
        PlayerNetwork.Instance.VoteForPlayer(name);
        GameObject.FindGameObjectWithTag("papercanvasloup").SetActive(false);
    }

    public void AddVoteJour(String name)
    {
        isVotedJour = true;
        PlayerNetwork.Instance.VoteForPlayerJour(name);
        GameObject.FindGameObjectWithTag("papercanvasjour").SetActive(false);
    }

    public void VoteIsFinish()
    {
       // VoteCanvas.SetActive(false);
        PlayerNetwork.Instance.PlayerVotesMap = null;
        PlayerNetwork.Instance.PlayerVotesMap = new Hashtable();
    
        InitializeVote();
    }


    public void InitializeVote()
    {
        foreach (var player in PhotonNetwork.playerList)
        {
            ListPlayers.Add(player.ID + "");
        }
        foreach (String p in ListPlayers)
        {
            PlayerNetwork.Instance.PlayerVotesMap[p] = 0;
        }
    }
}
