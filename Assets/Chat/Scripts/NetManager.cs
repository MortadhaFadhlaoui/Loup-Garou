using System.Collections;
using System.Collections.Generic;
using ExitGames.Client.Photon;
using UnityEngine;
using UnityEngine.VR;
using UnityEngine.XR;

public class NetManager : Photon.PunBehaviour {

    public GameObject avatarPrefab;
    public const string APP_VERSION = "1.0";
    //internal static string roomName = "Default";
    public byte MaxPlayersInRoom = 4 ;

    // Use this for initialization
    void Start () {
        //Debug.Log("roomName" + roomName);
        
        PhotonNetwork.ConnectUsingSettings(APP_VERSION);
        var temp = PhotonVoiceNetwork.Client;
        //StartCoroutine(LoadDevice("cardboard"));
        PhotonPlayer admin = new PhotonPlayer(false,123,"admin");
        PhotonNetwork.SetMasterClient(admin);
    }

    public override void OnConnectedToMaster()
    {
        Debug.Log("NetManager:OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinOrCreateRoom("LOUP", new RoomOptions() { MaxPlayers = MaxPlayersInRoom }, null);
    }
     
    public override void OnJoinedLobby()
    {
        Debug.Log("NetManager:OnConnectedToMaster() was called by PUN");
        PhotonNetwork.JoinOrCreateRoom("LOUP", new RoomOptions() { MaxPlayers = MaxPlayersInRoom }, null);
    }

    public override void OnDisconnectedFromPhoton()
    {
        Debug.LogWarning("NetManager:OnDisconnectedFromPhoton() was called by PUN");
    }

    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg)
    {
        Debug.Log("NetManager:OnPhotonRandomJoinFailed() was called by PUN");
        PhotonNetwork.JoinOrCreateRoom("LOUP", new RoomOptions() { MaxPlayers = MaxPlayersInRoom }, null);
    }

    public override void OnJoinedRoom()
    {      
      // PhotonVoiceRecorder rec;
            Debug.Log("NetManager: " + PhotonNetwork.player.UserId + " OnJoinedRoom() called by PUN.");
        GameObject go = PhotonNetwork.Instantiate(avatarPrefab.name,Vector3.zero,Quaternion.identity , 0);
        go.name = PhotonNetwork.player.UserId;
        
    }

}
