using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using UnityEngine.Networking.Types;
using UnityEngine.Networking.Match;
using System.Collections;
using UnityEngine.Networking.NetworkSystem;
using System.Collections.Generic;
using System;

namespace Prototype.NetworkLobby
{
   
    public class LobbyManager : NetworkLobbyManager 
    {
        public GameObject panel, menuplay, lobbypanel;
        public Dictionary<int, int> currentPlayers = new Dictionary<int, int>();

        static short MsgKicked = MsgType.Highest + 1;

        static public LobbyManager s_Singleton;

       
        [Header("Unity UI Lobby")]
        [Tooltip("Time in second between all players ready & match start")]
        public float prematchCountdown = 5.0f;

        //[Space]
        //[Header("UI Reference")]
        //public LobbyTopPanel topPanel;

        //public RectTransform mainMenuPanel;
        public RectTransform lobbyPanel;
        public GameObject MenuFindAGame;
        //public LobbyInfoPanel infoPanel;
        public LobbyCountdownPanel countdownPanel;
        //public GameObject addPlayerButton;
       // int prefabIndex = 1;
        protected RectTransform currentPanel;

        //public Button backButton;

        //public Text statusInfo;
        //public Text hostInfo;

        //Client numPlayers from NetworkManager is always 0, so we count (throught connect/destroy in LobbyPlayer) the number
        //of players, so that even client know how many player there is.
        [HideInInspector]
        public int _playerNumber = 0;

        //used to disconnect a client properly when exiting the matchmaker
        [HideInInspector]
        public bool _isMatchmaking = false;

        protected bool _disconnectServer = false;
        
        protected ulong _currentMatchID;

        protected LobbyHook _lobbyHooks;

        void Start()
        {           
            s_Singleton = this;
            _lobbyHooks = GetComponent<Prototype.NetworkLobby.LobbyHook>();
            //currentPanel = mainMenuPanel;
            //
            //backButton.gameObject.SetActive(false);
            GetComponent<Canvas>().enabled = true;         
            DontDestroyOnLoad(gameObject);

            SetServerInfo("Offline", "None");
        }

        public void ChangeTo(RectTransform newPanel)
        {
            if (currentPanel != null)
            {
                currentPanel.gameObject.SetActive(false);
            }

            if (newPanel != null)
            {
                newPanel.gameObject.SetActive(true);
            }
            else
            {
              //  backButton.gameObject.SetActive(false);
                SetServerInfo("Offline", "None");
                _isMatchmaking = false;
            }
        }

        public override void OnMatchList(bool success, string extendedInfo, List<MatchInfoSnapshot> matches)
        {
            base.OnMatchList(success, extendedInfo, matches);
            if (success)
            {
                if (matches.Count != 0)
                {
                    //Debug.Log("A list of matches was returned");

                    //join the last server (just in case there are two...)
                    MenuFindAGame.SetActive(false);
                    LobbyManager.singleton.matchMaker.JoinMatch(matches[matches.Count - 1].networkId, "", "", "", 0, 0, OnMatchJoined);
                }
                else
                {
                    AndroidNativeFunctions.ShowToast("No matches in requested room!");
                    Debug.Log("No matches in requested room!");
                }
            }
            else
            {
                Debug.LogError("Couldn't connect to match maker");
            }
        }


        public void DisplayIsConnecting()
        {
            var _this = this;
           // infoPanel.Display("Connecting...", "Cancel", () => { _this.backDelegate(); });
        }

        public void SetServerInfo(string status, string host)
        {
            Debug.Log("SetServerInfo" + status + host);
           /* statusInfo.text = status;
            hostInfo.text = host;*/
        }


        public delegate void BackButtonDelegate();
        public BackButtonDelegate backDelegate;
        public void GoBackButton()
        {
            
            backDelegate();
			//topPanel.isInGame = false;
        }

        // ----------------- Server management

        public void AddLocalPlayer()
        {
            TryToAddPlayer();
        }

        public void RemovePlayer(LobbyPlayer player)
        {
            player.RemovePlayer();
        }

        public void SimpleBackClbk()
        {
           // ChangeTo(mainMenuPanel);
        }
                 
        public void StopHostClbk()
        {
            Debug.Log("StopHostClbk");
            if (_isMatchmaking)
            {
                Debug.Log("_isMatchmaking");
                matchMaker.DestroyMatch((NetworkID)_currentMatchID, 0, OnDestroyMatch);
				_disconnectServer = true;
            }
            else
            {
                StopHost();
            }

           
          //  ChangeTo(mainMenuPanel);
        }

        public void StopClientClbk()
        {
            StopClient();

            if (_isMatchmaking)
            {
                StopMatchMaker();
            }

           // ChangeTo(mainMenuPanel);
        }

        public void StopServerClbk()
        {
            StopServer();
          //  ChangeTo(mainMenuPanel);
        }

        class KickMsg : MessageBase { }
        public void KickPlayer(NetworkConnection conn)
        {
            conn.Send(MsgKicked, new KickMsg());
        }




        public void KickedMessageHandler(NetworkMessage netMsg)
        {
           // infoPanel.Display("Kicked by Server", "Close", null);
            netMsg.conn.Disconnect();
        }

        //===================

        public override void OnStartHost()
        {
            base.OnStartHost();

            ChangeTo(lobbyPanel);
            backDelegate = StopHostClbk;
            SetServerInfo("Hosting", networkAddress);
        }

		public override void OnMatchCreate(bool success, string extendedInfo, MatchInfo matchInfo)
		{
			base.OnMatchCreate(success, extendedInfo, matchInfo);
            _currentMatchID = (System.UInt64)matchInfo.networkId;
		}



        public override void OnDestroyMatch(bool success, string extendedInfo)
		{
            Debug.Log("Match Destroyed !");
            
			base.OnDestroyMatch(success, extendedInfo);
			if (_disconnectServer)
            {
                StopMatchMaker();
                StopHost();
            }
        }

        //allow to handle the (+) button to add/remove player
        public void OnPlayersNumberModified(int count)
        {
            _playerNumber += count;

            int localPlayerCount = 0;
            foreach (PlayerController p in ClientScene.localPlayers)
                localPlayerCount += (p == null || p.playerControllerId == -1) ? 0 : 1;

            //addPlayerButton.SetActive(localPlayerCount < maxPlayersPerConnection && _playerNumber < maxPlayers);
        }

        // ----------------- Server callbacks ------------------

        //we want to disable the button JOIN if we don't have enough player
        //But OnLobbyClientConnect isn't called on hosting player. So we override the lobbyPlayer creation
        public override GameObject OnLobbyServerCreateLobbyPlayer(NetworkConnection conn, short playerControllerId)
        {
            if (!currentPlayers.ContainsKey(conn.connectionId))
            {
                print("OnLobbyServerCreateLobbyPlayer");
                currentPlayers.Add(conn.connectionId, 0);
                
            }                   

            GameObject obj = Instantiate(lobbyPlayerPrefab.gameObject) as GameObject;

            LobbyPlayer newPlayer = obj.GetComponent<LobbyPlayer>();
            newPlayer.ToggleJoinButton(numPlayers + 1 >= minPlayers);


            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers + 1 >= minPlayers);
                }
            }

            return obj;
        }

         public void SetPlayerTypeLobby(NetworkConnection conn, int type)
        {
            print("SetPlayerTypeLobby");
            if (currentPlayers.ContainsKey(conn.connectionId))
            currentPlayers[conn.connectionId] = type;
        }
        //Called on server.
        //This allows customization of the creation of the GamePlayer object on the server.
        //By default the gamePlayerPrefab is used to create the game-player, but this function allows that behaviour
        //to be customized.The object returned from the function will be used to replace the lobby-player on the connection.
            public override GameObject OnLobbyServerCreateGamePlayer(NetworkConnection conn, short playerControllerId)
            {
                //default player prefab index
                int index = currentPlayers[conn.connectionId];
                GameObject playerPrefab = (GameObject)GameObject.Instantiate(spawnPrefabs[index],  startPositions[conn.connectionId].position, Quaternion.identity);
                return playerPrefab;
            }
     


        /*   public override void OnServerAddPlayer(NetworkConnection conn, short playerControllerId, NetworkReader extraMessageReader)
           {

               int id = 2;

               if (extraMessageReader != null)
               {
                   IntegerMessage i = extraMessageReader.ReadMessage<IntegerMessage>();
                   id = i.value;
               }

               GameObject gamePlayerPrefab = spawnPrefabs[id];

               GameObject player;
               Transform startPos = GetStartPosition();
               if (startPos != null)
               {
                   player = (GameObject)Instantiate(gamePlayerPrefab, startPos.position, startPos.rotation);
               }
               else
               {
                   player = (GameObject)Instantiate(gamePlayerPrefab, Vector3.zero, Quaternion.identity);
               }

               NetworkServer.AddPlayerForConnection(conn, player, playerControllerId);
           }
           */

        public override void OnLobbyServerPlayerRemoved(NetworkConnection conn, short playerControllerId)
        {
            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers + 1 >= minPlayers);
                }
            }
        }

        public override void OnLobbyServerDisconnect(NetworkConnection conn)
        {
            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                LobbyPlayer p = lobbySlots[i] as LobbyPlayer;

                if (p != null)
                {
                    p.RpcUpdateRemoveButton();
                    p.ToggleJoinButton(numPlayers >= minPlayers);
                }
            }

        }

        public override bool OnLobbyServerSceneLoadedForPlayer(GameObject lobbyPlayer, GameObject gamePlayer)
        {
            //This hook allows you to apply state data from the lobby-player to the game-player
            //just subclass "LobbyHook" and add it to the lobby object.

            if (_lobbyHooks)
                _lobbyHooks.OnLobbyServerSceneLoadedForPlayer(this, lobbyPlayer, gamePlayer);

            return true;
        }

        // --- Countdown management

        public override void OnLobbyServerPlayersReady()
        {
			bool allready = true;
			for(int i = 0; i < lobbySlots.Length; ++i)
			{
           
                if (lobbySlots[i] != null)
					allready &= lobbySlots[i].readyToBegin;                  
            }

            if (allready)               
           
            StartCoroutine(ServerCountdownCoroutine());
        }

        public IEnumerator ServerCountdownCoroutine()
        {
            float remainingTime = prematchCountdown;
            int floorTime = Mathf.FloorToInt(remainingTime);

            while (remainingTime > 0)
            {
                yield return null;

                remainingTime -= Time.deltaTime;
                int newFloorTime = Mathf.FloorToInt(remainingTime);

                if (newFloorTime != floorTime)
                {//to avoid flooding the network of message, we only send a notice to client when the number of plain seconds change.
                    floorTime = newFloorTime;

                    for (int i = 0; i < lobbySlots.Length; ++i)
                    {
                        if (lobbySlots[i] != null)
                        {//there is maxPlayer slots, so some could be == null, need to test it before accessing!
                            (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(floorTime);
                        }
                    }
                }
            }

            for (int i = 0; i < lobbySlots.Length; ++i)
            {
                if (lobbySlots[i] != null)
                {
                    (lobbySlots[i] as LobbyPlayer).RpcUpdateCountdown(0);
                }
            }
            print("ogggk");
          
            ServerChangeScene(playScene);
        }

        public override void OnClientSceneChanged(NetworkConnection conn)
        {
            print("OnClientSceneChanged");
            GetComponent<Canvas>().enabled = false;
            base.OnClientSceneChanged(conn);
        }
        public override void ServerChangeScene(string sceneName)
        {
            print("OnClientSceneChanged");
            GetComponent<Canvas>().enabled = false;
            base.ServerChangeScene(sceneName);

        }
        // ----------------- Client callbacks ------------------

        public override void OnClientConnect(NetworkConnection conn)
        {
            print(conn);
            base.OnClientConnect(conn);
           /* IntegerMessage msg = new IntegerMessage(prefabIndex);
            if (!clientLoadedScene)
            {
                // Ready/AddPlayer is usually triggered by a scene load completing. if no scene was loaded, then Ready/AddPlayer it here instead.
                ClientScene.Ready(conn);
                print("ClientScene.Ready(conn)");
                if (autoCreatePlayer)
                {
                    print("autoCreatePlayer");
                    ClientScene.AddPlayer(conn, 0, msg);
                }
            }*/
            //infoPanel.gameObject.SetActive(false);

            conn.RegisterHandler(MsgKicked, KickedMessageHandler);

            if (!NetworkServer.active)
            {//only to do on pure client (not self hosting client)
                ChangeTo(lobbyPanel);
                backDelegate = StopClientClbk;
                SetServerInfo("Client", networkAddress);
            }
        }


        public override void OnClientDisconnect(NetworkConnection conn)
        {
            Debug.Log("OnClientDisconnect");
            base.OnClientDisconnect(conn);
            //ChangeTo(mainMenuPanel);
        }

        public override void OnClientError(NetworkConnection conn, int errorCode)
        {
            Debug.Log("OnClientError " + errorCode);
           // ChangeTo(mainMenuPanel);
           // infoPanel.Display("Cient error : " + (errorCode == 6 ? "timeout" : errorCode.ToString()), "Close", null);
        }
    }
}
