using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CurrentRoomCanvas : MonoBehaviour {

    //PlayerName
    private InputField textname;
    private InputField textnamel;
    private GameObject textnameg;

    private GameObject edittext;

    private PhotonView PhotonView;
    private GameObject btnlocalg, waitingforatherplayersg;
    private Button btnglobal;
    private Button btnlocal;
    private GameObject[] allplayerbtn;
    public Sprite non;
    public Sprite oui;
    public GameObject PlayerList, LeftRoom, SelectHero, CurrentHero, Select, text, vr;
    private IEnumerator coroutine;
    public Button BtnStart;
    public static int NbPlayerReady = 0;
    private int inx = 0;
    public static List<string> magical;
    public static List<string> roles;
    private Text waitingforatherplayers;

    private float prematchCountdown = 5.0f;
    private bool start = false;
    private void Awake()
    {
        PhotonView = GetComponent<PhotonView>();
        waitingforatherplayersg = GameObject.FindGameObjectWithTag("waitingforatherplayers");
        waitingforatherplayers = waitingforatherplayersg.GetComponent<Text>();
    }
    private void Update()
    {
        if (start == true)
        {
            PhotonView.RPC("RPC_CountDown", PhotonTargets.AllBuffered, PhotonNetwork.player);
            if (prematchCountdown <= 0)
            {
                NbPlayerReady = 0;
                PhotonNetwork.LoadLevel(3);
            }
        }
    }
    public void OnClickStartSync()
    {
        PhotonNetwork.LoadLevel(3);
    }

    public void OnClickStartDelayed()
    {
      
        if (!PhotonNetwork.isMasterClient)
            return;
        start = true;
        PhotonNetwork.room.IsOpen = false;
        PhotonNetwork.room.IsVisible = false;
    }



    public void OnReadyButton()
    {     
        PhotonView.RPC("RPC_PlayerGetReady", PhotonTargets.AllBuffered, PhotonNetwork.player);
    }

    public void OnReadyState(bool k)
    {
        if (k)
        {
            PhotonView.RPC("RPC_ReadyStateLeave", PhotonTargets.AllBuffered, PhotonNetwork.player);
        }  

    }

    [PunRPC]
    private void RPC_PlayerGetReady(PhotonPlayer photonPlayer)
    {

        textnameg = GameObject.Find("PlayerName" + photonPlayer.ID.ToString());
        textname = textnameg.GetComponent<InputField>();
        textname.text = photonPlayer.NickName;
        textname.enabled = false;
        Debug.Log(photonPlayer.NickName + " Is Ready");
        btnlocalg = GameObject.Find(photonPlayer.ID.ToString());
        btnlocal = btnlocalg.GetComponent<Button>();
        btnlocal.interactable = true;
        btnlocal.GetComponent<Image>().sprite = oui;      
        btnlocal.GetComponentInChildren<Text>().text = "Ready";
        btnlocal.GetComponent<Image>().color = new Color(0.0f, 204.0f / 255.0f, 204.0f / 255.0f, 1.0f);      
        allplayerbtn = GameObject.FindGameObjectsWithTag("aa");
        NbPlayerReady++;       
        if (NbPlayerReady == PhotonNetwork.room.MaxPlayers)
        {
            magical = new List<string>();
            magical.Add("VVOYANTE");
            magical.Add("VSORCIERE");
            magical.Add("VSALVATEUR");
            Debug.Log(PhotonNetwork.playerList.Length);
            if (true)
            {
                roles = new List<string>();
                roles.Add("L");
                roles.Add("L");
                roles.Add("V");
                roles.Add("V");


            }
            else if (PhotonNetwork.playerList.Length == 8)
            {
                roles = new List<string>();
                roles.Add("L");
                roles.Add("L");
                roles.Add("V");
                roles.Add("L");
                roles.Add(magical[0]);
                roles.Add("V");
                roles.Add("L");
                roles.Add("V");




            }
            else if (PhotonNetwork.playerList.Length == 10)
            {
                roles = new List<string>();
                roles.Add("L");
                roles.Add(magical[1]);
                roles.Add("L");
                roles.Add("V");
                roles.Add("V");
                roles.Add("L");
                roles.Add("L");
                roles.Add("L");
                roles.Add(magical[0]);
                roles.Add("V");





            }
            else
            {
                roles = new List<string>();
                roles.Add(magical[1]);
                roles.Add("L");
                roles.Add("L");
                roles.Add("L");
                roles.Add("V");
                roles.Add("L");
                roles.Add("V");
                roles.Add(magical[0]);
                roles.Add("L");
                roles.Add("L");
                roles.Add(magical[2]);
                roles.Add("V");

            }
        }           


        if (PhotonNetwork.isMasterClient)
        {
            if (NbPlayerReady == PhotonNetwork.room.MaxPlayers)
            {
                BtnStart.gameObject.SetActive(true);
            }
        }
                     
    }
    [PunRPC]
    private void RPC_CountDown(PhotonPlayer photonPlayer)
    {
        Debug.Log("prematchCountdown" + prematchCountdown);
        prematchCountdown -= Time.deltaTime;
        int myBlubb = (int)prematchCountdown;
        waitingforatherplayers.text = "your game will begin soon ";
        PlayerList.gameObject.SetActive(false);
        LeftRoom.gameObject.SetActive(false);
        SelectHero.gameObject.SetActive(false);
        vr.gameObject.SetActive(true);
        text.gameObject.SetActive(true);
        CurrentHero.gameObject.SetActive(false);
        Select.gameObject.SetActive(false);
        BtnStart.gameObject.SetActive(false);
    }

    [PunRPC]
    private void RPC_ReadyStateJoin(PhotonPlayer photonPlayer) 
    {
        allplayerbtn = GameObject.FindGameObjectsWithTag("aa");
        btnlocalg = GameObject.Find(PhotonNetwork.player.ID.ToString());
        edittext = GameObject.Find("PlayerName" + PhotonNetwork.player.ID.ToString());
        if (PhotonNetwork.room.PlayerCount == PhotonNetwork.room.MaxPlayers)
        {
            foreach (GameObject t in allplayerbtn)
            {

                //PlayerName
                textnameg = GameObject.Find("PlayerName" + t.name);
                textname = textnameg.GetComponent<InputField>();
                textname.enabled = false;

                btnglobal = t.GetComponent<Button>();
                btnglobal.GetComponentInChildren<Text>().text = "Not Ready";
                btnglobal.GetComponent<Image>().color = new Color(34.0f / 255.0f, 44 / 255.0f, 55.0f / 255.0f, 1.0f);
            }
            btnlocal = btnlocalg.GetComponent<Button>();
            btnlocal.interactable = true;
            btnlocal.GetComponent<Image>().sprite = oui;
            btnlocal.GetComponent<Image>().color = new Color(255.0f / 255.0f, 0.0f, 101.0f / 255.0f, 1.0f);
            btnlocal.GetComponentInChildren<Text>().text = "Join";
            waitingforatherplayers.text = "Be Ready to Start";
            Debug.Log("full room");
            textnamel = edittext.GetComponent<InputField>();
            textnamel.enabled = true;
        }
       
    }

    [PunRPC]
    private void RPC_ReadyStateLeave(PhotonPlayer photonPlayer)
    {      
        allplayerbtn = GameObject.FindGameObjectsWithTag("aa");
        btnlocalg = GameObject.Find(PhotonNetwork.player.ID.ToString());

        foreach (GameObject t in allplayerbtn)
            {           

            btnglobal = t.GetComponent<Button>();
                btnglobal.interactable = false;
                btnglobal.GetComponentInChildren<Text>().text = "...";
                btnglobal.GetComponent<Image>().sprite = non;               
                waitingforatherplayers.text = "waiting for ather players";
             }
            Debug.Log("NOT full room");
        if (PhotonNetwork.isMasterClient)
        {   
                BtnStart.gameObject.SetActive(false);          
        }
        NbPlayerReady = 0;
    }
  
    public void OnJoinedRoom()
    {           
        Debug.Log("Joined Room");
        StartCoroutine(WaitAndPrint());
    }

    public void OnLeftRoom()
    {       
        Debug.Log("leaving");
    }
    private IEnumerator WaitAndPrint()
    {             
        yield return new WaitForSeconds(0.01f);
        PhotonView.RPC("RPC_ReadyStateJoin", PhotonTargets.AllBuffered, PhotonNetwork.player);
    }


  
}
