using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateOrJoin : MonoBehaviour
{
  [SerializeField]
  private CreateRoomMenu _createRoomMenu;
  [SerializeField]
  private RoomsListingMenu _roomsListingMenu;
  private RoomsCanvases _roomCanvases;
  // Start is called before the first frame update
  public void Initialize(RoomsCanvases canvases)
  {
    _roomCanvases = canvases;
    _createRoomMenu.Initialize(canvases);
    _roomsListingMenu.Initialize(canvases);
  }

  public void Show()
  {
    gameObject.SetActive(true);
    _roomCanvases.CurrentRoom.Hide();
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }
}
