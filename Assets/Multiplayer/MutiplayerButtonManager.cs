using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class MutiplayerButtonManager : MonoBehaviourPunCallbacks
{
  public override void OnLeftRoom()
  {
    SceneManager.LoadScene("GameMenu");
  }

  public void LeaveRoom()
  {
    PhotonNetwork.LeaveRoom();
  }
}
