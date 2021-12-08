using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitingMenu : MonoBehaviour
{
  public OtherPlayerStatus playerStatus;

  void Update()
  {
    playerStatus.OnWait_CheckOtherPlayerReady();
  }
}
