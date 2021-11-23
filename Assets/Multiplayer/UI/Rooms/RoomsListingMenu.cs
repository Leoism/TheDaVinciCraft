using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class RoomsListingMenu : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Transform _content;
  [SerializeField]
  private RoomListing _roomListing;
  private List<RoomListing> _listings = new List<RoomListing>();
  private RoomsCanvases _roomCanvases;

  public void Initialize(RoomsCanvases roomsCanvases)
  {
    _roomCanvases = roomsCanvases;
  }

  public override void OnJoinedRoom()
  {
    _roomCanvases.CurrentRoom.Show();
    for (int i = 0; i < _content.childCount; i++)
    {
      Destroy(_content.GetChild(i).gameObject);
    }

    _listings.Clear();
  }

  public override void OnRoomListUpdate(List<RoomInfo> roomList)
  {
    foreach (RoomInfo roomInfo in roomList)
    {
      if (roomInfo.RemovedFromList)
      {
        int idx = _listings.FindIndex(listing => listing.RoomInfo.Name.Equals(roomInfo.Name));
        if (idx >= 0)
        {
          Destroy(_listings[idx].gameObject);
          _listings.RemoveAt(idx);
        }
      }
      else
      {
        int idx = _listings.FindIndex(listing => listing.RoomInfo.Name.Equals(roomInfo.Name));
        if (idx != -1) return;
        RoomListing listing = Instantiate(_roomListing, _content);
        listing.SetRoomInfo(roomInfo);
        _listings.Add(listing);
      }
    }
  }
}
