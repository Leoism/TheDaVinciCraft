using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayersListingMenu : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Transform _content;
  [SerializeField]
  private PlayerListing _playerListing;
  private List<PlayerListing> _listings = new List<PlayerListing>();

  void Awake()
  {
    GetCurrentRoomPlayers();
  }

  private void GetCurrentRoomPlayers()
  {
    if (!PhotonNetwork.InRoom) return;
    foreach (KeyValuePair<int, Photon.Realtime.Player> player in PhotonNetwork.CurrentRoom.Players)
    {
      AddPlayerListing(player.Value);
    }
  }

  private void AddPlayerListing(Photon.Realtime.Player newPlayer)
  {
    PlayerListing listing = Instantiate(_playerListing, _content);
    listing.SetPlayerInfo(newPlayer);
    _listings.Add(listing);
  }

  public override void OnPlayerEnteredRoom(Photon.Realtime.Player newPlayer)
  {
    AddPlayerListing(newPlayer);
  }

  public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
  {
    int idx = _listings.FindIndex(listing => listing.Player.Equals(otherPlayer));
    if (idx >= 0)
    {
      Destroy(_listings[idx].gameObject);
      _listings.RemoveAt(idx);
    }
  }
}
