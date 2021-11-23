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

  public override void OnRoomListUpdate(List<RoomInfo> roomList)
  {
    foreach (RoomInfo roomInfo in roomList)
    {
      if (roomInfo.RemovedFromList)
      {
        int idx = _listings.FindIndex(listing => listing.RoomInfo.Name == roomInfo.Name);
        if (idx >= 0)
        {
          Destroy(_listings[idx].gameObject);
          _listings.RemoveAt(idx);
        }
      }
      else
      {
        RoomListing listing = Instantiate(_roomListing, _content);
        listing.SetRoomInfo(roomInfo);
        _listings.Add(listing);
      }
    }
  }
}
