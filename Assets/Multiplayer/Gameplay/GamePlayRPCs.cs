using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GamePlayRPCs : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Timer timer;
  [PunRPC]
  void RPC_OnArtDestruction()
  {
    PhotonNetwork.LoadLevel("AlienWin");
  }

  [PunRPC]
  void RPC_OnClick_Done()
  {
    Debug.Log("Hello");
    timer.finishState();
  }

  [PunRPC]
  void RPC_AlienOutOfWeapons()
  {
    PhotonNetwork.LoadLevel("HumanWin");
  }
}
