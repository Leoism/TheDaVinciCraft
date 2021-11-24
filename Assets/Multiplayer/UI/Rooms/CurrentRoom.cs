using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class CurrentRoom : MonoBehaviour
{
  private RoomsCanvases _roomCanvases;
  [SerializeField]
  private Button _startGameButton;
  [SerializeField]
  private PlayersListingMenu _playersListingMenu;
  [SerializeField]
  private LeaveRoomButton _leaveRoomButton;
  public LeaveRoomButton LeaveRoomButton { get { return _leaveRoomButton; } }
  // Start is called before the first frame update
  public void Initialize(RoomsCanvases canvases)
  {
    _roomCanvases = canvases;
    _playersListingMenu.Initialize(canvases);
    _leaveRoomButton.Initialize(canvases);

  }

  void Start()
  {
    _startGameButton.gameObject.SetActive(PhotonNetwork.IsMasterClient);
  }

  public void Show()
  {
    gameObject.SetActive(true);
    _roomCanvases.CreateOrJoin.Hide();
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }
}
