using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyCanvas : MonoBehaviour {

    [SerializeField]
    private RoomLayoutGroup _roomLayoutGroup;
    private RoomLayoutGroup RoomLayoutGroup
    {
        get { return _roomLayoutGroup; }
    }
    public GameObject CurrentRoom, lobby,btnback;


    public void getit()
    {
        RoomLayoutGroup.OnReceivedRoomListUpdate();
    }

    public void OnClickJoinRoom(string roomName)
    {
        if (PhotonNetwork.JoinRoom(roomName))
        {
            CurrentRoom.gameObject.SetActive(true);
            lobby.gameObject.SetActive(false);
            btnback.gameObject.SetActive(false);
        }
        else
        {
            print("Join room failed.");
        }
    }
}
