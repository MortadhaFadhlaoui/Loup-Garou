using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class LobbyNetwork : MonoBehaviour
{
    public static LobbyNetwork instance;
    public List<Vector3> listInt = new List<Vector3> { new Vector3(114, 5, 317), new Vector3(111, 5, 315), new Vector3(114, 5, 317), new Vector3(112, 5, 322), new Vector3(109, 5, 320), new Vector3(109, 5, 316), new Vector3(108, 5, 318), new Vector3(114, 5, 322) };


    void Start()
    {
        // var temp = PhotonVoiceNetwork.Client;
        if (!PhotonNetwork.connected)
        {

            PhotonNetwork.ConnectUsingSettings("0.0.0");
            print("connecting to the server");
        }
    }


    public void OnConnectedToMaster()
    {
        print("connection master");
        PhotonNetwork.playerName = PlayerNetwork.Instance.PlayerName;
        Debug.Log(PhotonNetwork.playerName);
        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.JoinLobby(TypedLobby.Default);
    }

    public void OnJoinedLobby()
    {

        Debug.Log("Joined Lobby");
        if (!PhotonNetwork.inRoom)
        {
            MainCanvasManager.Instance.LobbyCanvas.transform.SetAsLastSibling();
        }
    }

    void Awake()
    {
        if (instance != null)
        {
            DestroyImmediate(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        instance = this;
    }

    

    public void OnPhotonCreateRoomFailed(object[] codeAndMessage)
    {
        print("create room failed: " + codeAndMessage[1]);
    }

    public void OnCreatedRoom()
    {
        Debug.Log("Created Room ");
    }



   
    public void OnReceivedRoomListUpdate()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        foreach (RoomInfo room in rooms)
        {
            Debug.Log("Room name1: " + room.Name);
        }


    }


    public void LeftRoom()
    {
        if (PhotonNetwork.inRoom)
        {
            PhotonNetwork.LeaveRoom();

        }
        else
        {
            Debug.Log("not in room");
        }
    }


}