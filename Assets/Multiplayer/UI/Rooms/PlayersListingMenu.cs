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
  private RoomsCanvases _roomsCanvases;

  public void Initialize(RoomsCanvases canvases)
  {
    _roomsCanvases = canvases;
  }

  public override void OnEnable()
  {
    base.OnEnable();
    GetCurrentRoomPlayers();

  }

  public override void OnDisable()
  {
    base.OnDisable();
    for (int i = 0; i < _listings.Count; i++)
    {
      Destroy(_listings[i].gameObject);
    }
    _listings.Clear();
  }

  public void OnClick_StartGame()
  {
    if (PhotonNetwork.IsMasterClient && PhotonNetwork.CurrentRoom.PlayerCount == 2)
    {
      Player humanPlayer = new Player();
      Player alienPlayer = new Player();
      humanPlayer.name = PhotonNetwork.LocalPlayer.NickName;
      humanPlayer.type = "(Human)";
      alienPlayer.name = PhotonNetwork.CurrentRoom.Players[1].NickName;
      alienPlayer.type = "(Alien)";
      GameManager.globalManager.Reset();
      GameManager.globalManager.SetPlayers(humanPlayer, alienPlayer);
      PhotonNetwork.CurrentRoom.IsOpen = false;
      PhotonNetwork.CurrentRoom.IsVisible = false;
      PhotonNetwork.LoadLevel("BuyingMenu");
    }
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
    int idx = _listings.FindIndex(listing => listing.Player == newPlayer);
    if (idx >= 0)
    {
      _listings[idx].SetPlayerInfo(newPlayer);
    }
    else
    {
      PlayerListing listing = Instantiate(_playerListing, _content);
      listing.SetPlayerInfo(newPlayer);
      _listings.Add(listing);
    }
  }

  public override void OnMasterClientSwitched(Photon.Realtime.Player newMasterClient)
  {
    _roomsCanvases.CurrentRoom.LeaveRoomButton.OnClick_Leave();
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
