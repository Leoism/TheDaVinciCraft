using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class GamePlayRPCs : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Timer timer;
  [SerializeField]
  private NetworkSFXPlayer networkSFXPlayer;
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

  [PunRPC]
  void RPC_PlayBoomerang()
  {
    networkSFXPlayer.PlayClip("Boomerang");
  }

  [PunRPC]
  void RPC_PlayRay()
  {
    networkSFXPlayer.PlayClip("Ray");
  }

  [PunRPC]
  void RPC_PlayCatapult()
  {
    networkSFXPlayer.PlayClip("Catapult");
  }

  [PunRPC]
  void RPC_StopAudio()
  {
    networkSFXPlayer.Stop();
  }
}
