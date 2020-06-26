using Photon.Pun;
using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : MonoBehaviour
{
    [SerializeField] InputField playerNamefield = null;
    [SerializeField] Button continueButton = null;

    private const string playerPrefNameKey = "PlayerName";
    void Start()
    {
        SetUpInputField();
    }

    private void SetUpInputField()
    {
        if(!PlayerPrefs.HasKey(playerPrefNameKey))
        {
            return;
        }

        string defaultName = PlayerPrefs.GetString(playerPrefNameKey);

        playerNamefield.text = defaultName;

    }

    public void SetPlayerName(string value)
    {
        continueButton.interactable = string.IsNullOrEmpty(value) ? false : true;

        if(!string.IsNullOrEmpty(value))
        {
            SavePlayerName();
        }
    }
    public void SavePlayerName()
    {
        string playerName = playerNamefield.text;

        PhotonNetwork.NickName = playerName;

        PlayerPrefs.SetString(playerPrefNameKey, playerName);
    }
}
