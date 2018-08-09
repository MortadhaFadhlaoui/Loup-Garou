using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerListing : MonoBehaviour
{

    public PhotonPlayer PhotonPlayer { get; private set; }
    public InputField PlayerName;
    public static string nameplayer;
    bool ok = true;
    private List<string> l = new List<string>();
    [SerializeField]
    private Text _playerPing;
    private Text m_playerPing
    {
        get { return _playerPing; }
    }


    public Button ReadyButton;
    public Text ReadyText;


    public void ApplyPhotonPlayer(PhotonPlayer photonPlayer)
    {

        PhotonPlayer = photonPlayer;
        PlayerName.text = photonPlayer.NickName;


        //PlayerName
        PlayerName.name = "PlayerName" + photonPlayer.ID.ToString();



        ReadyButton.name = photonPlayer.ID.ToString();
        StartCoroutine(C_ShowPing());
    }


    private void Start()
    {
        foreach (var item in PhotonNetwork.playerList)
        {
            if (!PhotonNetwork.player.NickName.Equals(item.NickName))
            {
                l.Add(item.NickName);
            }
        }
        //  Button button = GetComponent<Button>();        
        ReadyButton.onClick.AddListener(() => ChangeName());
        Debug.Log(PhotonNetwork.player.NickName);
    }

    //PlayerName
    public void ChangeName()
    {
        if (PlayerName.text.Equals(""))
        {
            AndroidNativeFunctions.ShowToast("Name is required");
        }
        else if (PlayerName.text.Length < 4)
        {
            AndroidNativeFunctions.ShowToast("Name is too Short");
        }
        else
        {
            foreach (var item in l)
            {
                if (item.Equals(PlayerName.text))
                {
                    ok = false;
                    break;
                }
                else
                {
                    ok = true;
                }
            }
            if (!ok)
            {
                Debug.Log("Name already exist");
                AndroidNativeFunctions.ShowToast("Name already exist");
            }
            else
            {

                PhotonNetwork.playerName = PlayerName.text;
                nameplayer = PlayerName.text;
                GameObject lobbyCanvasObj = MainCanvasManager.Instance.CurrentRoomCanvas.gameObject;
                if (lobbyCanvasObj == null)
                    return;
                CurrentRoomCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<CurrentRoomCanvas>();
                lobbyCanvas.OnReadyButton();
            }
        }


    }





    private IEnumerator C_ShowPing()
    {
        while (PhotonNetwork.connected)
        {
            int ping = (int)PhotonPlayer.CustomProperties["Ping"];
            m_playerPing.text = ping.ToString() + " ms";
            yield return new WaitForSeconds(1f);
        }

        yield break;
    }
}
