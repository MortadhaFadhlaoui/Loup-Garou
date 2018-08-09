using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class LobbyHome : MonoBehaviour {
    public Text RoomName;
    public Text JoinedRoomName;
    public Dropdown roomSizeDropDown;
    private int roomSize = 8;
    public GameObject lobby, currentroom, menufindagame, backbtn, MenuPlay;
    public List<Vector3> listInt = new List<Vector3> { new Vector3(114, 5, 317), new Vector3(111, 5, 315), new Vector3(114, 5, 317), new Vector3(112, 5, 322), new Vector3(109, 5, 320), new Vector3(109, 5, 316), new Vector3(108, 5, 318), new Vector3(114, 5, 322) };

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnClick_CreateRoom()
    {

        Debug.Log("Size selected" + roomSizeDropDown.value);
        switch (roomSizeDropDown.value)
        {
            case 0:
                roomSize = 8;
                break;
            case 1:
                roomSize = 10;
                break;
            case 2:
                roomSize = 12;
                break;
            case 3:
                roomSize = 14;
                break;
        }
        //MaxPlayers = (byte)roomSize
        RoomOptions roomOptions = new RoomOptions() { IsVisible = true, IsOpen = true, MaxPlayers = 3 };

        RoomInfo[] rooms = PhotonNetwork.GetRoomList();
        if (RoomName.text.Equals(""))
        {
            AndroidNativeFunctions.ShowToast("Please select Room Name");
            print("matchNameInput + faragh");
        }
        else
        {

            if (rooms.Length == 0)
            {
                Debug.Log("Room dos not exist");

                if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
                {

                    print("create room successfully sent.");
                    currentroom.gameObject.SetActive(true);
                    menufindagame.gameObject.SetActive(false);
                    backbtn.gameObject.SetActive(false);
                }
                else
                {
                    print("create room failed to send");
                }
            }
            else
            {
                foreach (RoomInfo room in rooms)
                {
                    if (RoomName.text.Equals(room.Name))
                    {
                        AndroidNativeFunctions.ShowToast("This room exists !");
                        Debug.Log("This room exists !");
                        break;
                    }
                    else
                    {
                        if (PhotonNetwork.CreateRoom(RoomName.text, roomOptions, TypedLobby.Default))
                        {

                            print("create room successfully sent.");
                            currentroom.gameObject.SetActive(true);
                            menufindagame.gameObject.SetActive(false);
                            backbtn.gameObject.SetActive(false);
                        }
                        else
                        {
                            print("create room failed to send");
                        }
                    }
                }
            }
        }
    }
    public void JoinGame()
    {
        RoomInfo[] rooms = PhotonNetwork.GetRoomList();

        if (rooms.Length == 0)
        {
            Debug.Log("Room dos not exist");
        }
        else
        {
            foreach (RoomInfo room in rooms)
            {
                if (JoinedRoomName.text.Equals(room.Name))
                {
                    if (room.PlayerCount < room.MaxPlayers)
                    {
                        Debug.Log(room.PlayerCount);
                        currentroom.gameObject.SetActive(true);
                        menufindagame.gameObject.SetActive(false);
                        backbtn.gameObject.SetActive(false);
                        PhotonNetwork.JoinRoom(room.Name);
                    }
                    else
                    {
                        AndroidNativeFunctions.ShowToast("Room in max players !");
                        Debug.Log("Room in max players: " + room.PlayerCount);
                    }

                }
                else
                {
                    Debug.Log("Room dos not exist");
                }
            }
        }

    }
    public void ListGames()
    {
        lobby.gameObject.SetActive(true);
        MenuPlay.gameObject.SetActive(false);
        GameObject lobbyCanvasObj = MainCanvasManager.Instance.LobbyCanvas.gameObject;
        if (lobbyCanvasObj == null)
            return;

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();
        lobbyCanvas.getit();
    }

}
