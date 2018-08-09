using GameSparks.Api.Requests;
using GameSparks.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoresMenu : MonoBehaviour {

    public GameObject ScorePanel,achevPanel,round,back;
    public Switch sw;
    public bool f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
    
	}

    public void HideMenu()
    {

        f = sw.isOn;
        if (f)
        {
            sw.isOn = true;
            back.SetActive(false);

            round.SetActive(false);
            ScorePanel.SetActive(false);
            achevPanel.SetActive(false);
           
        }
        else
        {
            sw.isOn = true;
            ScorePanel.SetActive(false);
            
            round.SetActive(false);
            back.SetActive(false);
            achevPanel.SetActive(false);
         
        }
       
    }

    public void ShowMenu()
    {
        back.SetActive(true);
        round.SetActive(true);
        ScorePanel.SetActive(true);
        
    }
    public void show()
    {
        f = sw.isOn;
        if (f)
        {
            achevPanel.SetActive(false);
            ScorePanel.SetActive(true);
        }
        else
        {
            achevPanel.SetActive(true);
            ScorePanel.SetActive(false);
        }
    }

    
}
