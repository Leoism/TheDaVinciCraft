using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomsCanvases : MonoBehaviour
{
  [SerializeField]
  private CreateOrJoin _createOrJoin;
  public CreateOrJoin CreateOrJoin { get { return _createOrJoin; } }
  [SerializeField]
  private CurrentRoom _currentRoom;
  public CurrentRoom CurrentRoom { get { return _currentRoom; } }
  [SerializeField]
  private LeaveRoomButton _leaveRoom;
  // Start is called before the first frame update
  void Awake()
  {
    Initialize();
  }

  void Initialize()
  {
    CreateOrJoin.Initialize(this);
    CurrentRoom.Initialize(this);
    _leaveRoom.Initialize(this);
    CreateOrJoin.Show();
    CurrentRoom.Hide();
  }
}
