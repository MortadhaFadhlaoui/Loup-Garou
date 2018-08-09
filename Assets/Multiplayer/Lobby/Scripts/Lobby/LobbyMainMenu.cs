using UnityEngine;
using UnityEngine.UI;
using System.Collections;

namespace Prototype.NetworkLobby
{
    //Main menu, mainly only a bunch of callback called by the UI (setup throught the Inspector)
    public class LobbyMainMenu : MonoBehaviour 
    {
        public LobbyManager lobbyManager;
        public Dropdown roomSizeDropDown;
        private int roomSize = 8;
        
        public RectTransform lobbyServerList;
        /*  public RectTransform lobbyPanel;*/

        // public InputField ipInput;
        public InputField matchNameJoinInput;
        public InputField matchNameInput;
        public GameObject MenuFindAGame;
        public GameObject MenuPlay;
        public void OnEnable()
        {
           // lobbyManager.topPanel.ToggleVisibility(true);

         /*   ipInput.onEndEdit.RemoveAllListeners();
            ipInput.onEndEdit.AddListener(onEndEditIP);

            matchNameInput.onEndEdit.RemoveAllListeners();
            matchNameInput.onEndEdit.AddListener(onEndEditGameName);*/
        }

        public void OnClickHost()
        {
            lobbyManager.StartHost();
        }

        public void OnClickJoin()
        {
           // lobbyManager.ChangeTo(lobbyPanel);

           // lobbyManager.networkAddress = ipInput.text;
            lobbyManager.StartClient();

            lobbyManager.backDelegate = lobbyManager.StopClientClbk;
            lobbyManager.DisplayIsConnecting();

            lobbyManager.SetServerInfo("Connecting...", lobbyManager.networkAddress);
        }

        public void OnClickDedicated()
        {
            lobbyManager.ChangeTo(null);
            lobbyManager.StartServer();

            lobbyManager.backDelegate = lobbyManager.StopServerClbk;

            lobbyManager.SetServerInfo("Dedicated Server", lobbyManager.networkAddress);
        }

        public void OnClickCreateMatchmakingGame()
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

            if (matchNameInput.text.Equals(""))
            {
                AndroidNativeFunctions.ShowToast("Please select Room Name");
                print("matchNameInput + faragh");
            }else
            {
                MenuFindAGame.SetActive(false);
                Debug.Log("CLICKED" + matchNameInput.text);
                Debug.Log("CLICKED" + roomSize);
                lobbyManager.StartMatchMaker();
                lobbyManager.matchMaker.CreateMatch(
                    matchNameInput.text,
                    (uint)roomSize,
                    true,
                    "", "", "", 0, 0,
                    lobbyManager.OnMatchCreate);

                lobbyManager.backDelegate = lobbyManager.StopHost;
                lobbyManager._isMatchmaking = true;
                lobbyManager.DisplayIsConnecting();

                lobbyManager.SetServerInfo("Matchmaker Host", lobbyManager.matchHost);
                //NetManager.roomName = matchNameInput.text;
            }
        }

        public void FindInternetMatch()
        {
            if (matchNameJoinInput.text.Equals(""))
            {
                AndroidNativeFunctions.ShowToast("Please enter name of room");
                print("matchNameJoinInput + faragh");
            }
            else
            {
                lobbyManager.StartMatchMaker();
                lobbyManager.matchMaker.ListMatches(0, 10, matchNameJoinInput.text, true, 0, 0, lobbyManager.OnMatchList);
            }

        }

        public void OnClickOpenServerList()
        {
            MenuPlay.SetActive(false);
            lobbyManager.StartMatchMaker();
            lobbyManager.backDelegate = lobbyManager.SimpleBackClbk;
            lobbyManager.ChangeTo(lobbyServerList);
        }

        void onEndEditIP(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickJoin();
            }
        }

        void onEndEditGameName(string text)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                OnClickCreateMatchmakingGame();
            }
        }

    }
}
