using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoomListing : MonoBehaviour {


    [SerializeField]
    private Text _slotsText;
    private Text SlotsText
    {
        get { return _slotsText; }
    }
    [SerializeField]
    private Text _roomNameText;
    private Text RoomNameText
    {
        get { return _roomNameText; }
    }
  

    public string RoomName { get; private set; }
    public string Slots { get; private set; }
    public Button JoinButton;
    public bool Updated { get; set; }

    private void Start()
    {
        GameObject lobbyCanvasObj = MainCanvasManager.Instance.LobbyCanvas.gameObject;
        if (lobbyCanvasObj == null)
            return;

        LobbyCanvas lobbyCanvas = lobbyCanvasObj.GetComponent<LobbyCanvas>();

        //  Button button = GetComponent<Button>();
        JoinButton.onClick.AddListener(() => lobbyCanvas.OnClickJoinRoom(RoomNameText.text));
    }

    private void OnDestroy()
    {
       // Button button = GetComponent<Button>();
        JoinButton.onClick.RemoveAllListeners();
    }

    public void SetRoomNameText(string text,string text1)
    {
        Slots = text1;
        SlotsText.text = Slots;

        RoomName = text;
        RoomNameText.text = RoomName;
    }



}
