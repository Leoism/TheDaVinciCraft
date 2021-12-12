using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Realtime;
using Photon.Pun;

public class CreateRoomMenu : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private InputField _roomName;

  private RoomsCanvases _roomsCanvases;

  public void Initialize(RoomsCanvases canvases)
  {
    _roomsCanvases = canvases;
  }

  public void OnClick_CreateRoom()
  {
    if (!PhotonNetwork.IsConnected) return;
    RoomOptions options = new RoomOptions { MaxPlayers = 2, EmptyRoomTtl = 0, PlayerTtl = 60000 };
    PhotonNetwork.CreateRoom(_roomName.text, options);
  }

  public override void OnCreatedRoom()
  {
    Debug.Log("Created Room");
    _roomsCanvases.CurrentRoom.Show();
  }

  public override void OnCreateRoomFailed(short returnCode, string message)
  {
    Debug.Log("Room creation failed");
  }
}
