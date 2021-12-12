using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class LeaveRoomButton : MonoBehaviour
{
  RoomsCanvases _roomCanvases;
  public void Initialize(RoomsCanvases canvases)
  {
    _roomCanvases = canvases;
  }

  public void OnClick_Leave()
  {
    if (PhotonNetwork.InRoom)
    {
      PhotonNetwork.LeaveRoom(false);
      _roomCanvases.CurrentRoom.Hide();
      _roomCanvases.CreateOrJoin.Show();
    }
  }
}
