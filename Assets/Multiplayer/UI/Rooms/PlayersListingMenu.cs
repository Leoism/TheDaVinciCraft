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
      // set up game manager
      GameManager.globalManager.Reset();
      GameManager.globalManager.isOnlineMode = true;
      Player[] players = CreatePlayers();
      // set who is the human and alien
      GameManager.globalManager.SetPlayers(players[0], players[1]);
      string modeToggle = _roomsCanvases.ModeGroup.toggleGroup.GetFirstActiveToggle().name;
      GameMode mode = modeToggle.StartsWith("Quick") ? GameMode.SHORT : (modeToggle.StartsWith("Standard") ? GameMode.STANDARD : GameMode.LONG);
      GameManager.globalManager.SetGameType(mode);
      // hide the room
      PhotonNetwork.CurrentRoom.IsOpen = false;
      PhotonNetwork.CurrentRoom.IsVisible = false;
      // send RPCs to other player
      base.photonView.RPC("RPC_SendGameInfo", RpcTarget.Others, mode);
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

  private Player[] CreatePlayers()
  {
    Player myself = new Player();
    Player other = new Player();
    myself.name = PhotonNetwork.LocalPlayer.NickName;
    other.name = PhotonNetwork.PlayerListOthers[0].NickName;
    myself.type = PhotonNetwork.IsMasterClient ? "(Human)" : "(Alien)";
    other.type = PhotonNetwork.IsMasterClient ? "(Alien)" : "(Human)";
    return new Player[] { myself, other };
  }

  [PunRPC]
  public void RPC_SendGameInfo(int mode)
  {
    GameManager.globalManager.Reset();
    GameManager.globalManager.isOnlineMode = true;
    Player[] players = CreatePlayers();
    GameManager.globalManager.SetPlayers(players[1], players[0]);
    GameManager.globalManager.SetGameType((GameMode)mode);
    new GameObject().AddComponent<MonitorConnection>();
    base.photonView.RPC("RPC_AlienFinishSettingPlayers", RpcTarget.MasterClient);
  }

  [PunRPC]
  public void RPC_AlienFinishSettingPlayers()
  {
    new GameObject().AddComponent<MonitorConnection>();
    PhotonNetwork.LoadLevel("BuyingMenu");
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
