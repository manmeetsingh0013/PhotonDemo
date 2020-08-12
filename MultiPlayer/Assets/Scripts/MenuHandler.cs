using Photon.Pun;
using Photon.Realtime;
using System;
using UnityEngine;
using UnityEngine.UI;

public class MenuHandler : MonoBehaviourPunCallbacks
{
    #region PRIVATE SERIALIZE FIELDS

    [SerializeField] private const int maxPlayerInRoom = 2;

    [SerializeField] GameObject statusPanel;

    [SerializeField] Text statusText;

    bool isConnecting = false;

    #endregion

    #region PRIVATE FIELDS

    private string gameVersion = "0.1";

    private bool isGamePlayLoaded = false;

    #endregion

    #region MONOBEHAVIOR METHODS

    private void Awake()
    {
        PhotonNetwork.AutomaticallySyncScene = true;

    }

    #endregion

    #region MONOBEHAVIOURPUN METHODS

    public override void OnConnectedToMaster()
    {
        Debug.Log("Connected to master");

        if (isConnecting)
        {
            PhotonNetwork.JoinRandomRoom();
        }
    }

    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.LogWarningFormat("Discounted to the master due to {0}", cause);
    }

    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("Room joining failed " + message);

        PhotonNetwork.CreateRoom(null,new RoomOptions { MaxPlayers = maxPlayerInRoom});
    }

    public override void OnJoinedRoom()
    {
        Debug.Log("Successfully join the room");

        statusText.text = "Room joining...";

        int playerCount = PhotonNetwork.CurrentRoom.PlayerCount;

        if(playerCount !=maxPlayerInRoom)
        {
            statusText.text = "waiting for opponent...";

            Debug.Log("client is waiting");

        }
        else
        {
            LoadGamePlay();
        }
    }
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        if(PhotonNetwork.CurrentRoom.PlayerCount==maxPlayerInRoom)
        {
            LoadGamePlay();
        }
    }
    #endregion

    #region PRIVATE METHODS

    private void LoadGamePlay()
    {
        if (!isGamePlayLoaded)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;

            statusText.text = "opponent found!";

            Debug.Log("Opponent found lets play now!");

            isGamePlayLoaded = true;

            PhotonNetwork.LoadLevel("Match");
        }
    }

    #endregion

    #region PUBLIC METHODS

    public void Connect()
    {

        isConnecting = true;

        statusPanel.SetActive(true); 

        statusText.text = "Connecting...";

        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
        }
        else
        {
            PhotonNetwork.ConnectUsingSettings();

            PhotonNetwork.GameVersion = gameVersion;
        }
    }

    #endregion
}
