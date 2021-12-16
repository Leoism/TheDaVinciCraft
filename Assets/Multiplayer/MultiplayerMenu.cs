using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
public class MultiplayerMenu : MonoBehaviourPunCallbacks
{
  string version = "1.0";
  public InputField nameField = null;

  public override void OnConnectedToMaster()
  {
    print("Connected to server");
    if (!PhotonNetwork.InLobby)
      PhotonNetwork.JoinLobby();
  }

  public override void OnDisconnected(DisconnectCause cause)
  {
    print("Disconnected: " + cause.ToString());
  }

  void Awake()
  {
    PhotonNetwork.AutomaticallySyncScene = true;
  }
  // Start is called before the first frame update
  void Start()
  {
    if (!PhotonNetwork.IsConnected)
    {
      PhotonNetwork.GameVersion = version;
      PhotonNetwork.PhotonServerSettings.AppSettings.FixedRegion = "usw";
      PhotonNetwork.ConnectUsingSettings();
    }
    PhotonNetwork.NickName = "Player " + GenerateRandomID(4) + GenerateRandomID(6);
  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetPlayerName(string value)
  {
    string inputValue = nameField.text;
    if (string.IsNullOrEmpty(inputValue.Trim()))
    {
      Debug.LogError("Player name is empty");
      return;
    }
    PhotonNetwork.NickName = inputValue;
  }
  public void CreateRoom()
  {
    string roomID = GenerateRandomID(6);
    PhotonNetwork.CreateRoom(roomID, new RoomOptions { MaxPlayers = 2 });
  }

  public void LoadAvailableRooms()
  {
    PhotonNetwork.JoinLobby();
  }

  private string GenerateRandomID(int length)
  {
    string id = "";
    string alphaBank = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
    for (int i = 0; i < length; i++)
    {
      id += alphaBank[(int)Random.Range(0, alphaBank.Length)];
    }
    return id;
  }
}
