using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RoundSaver : MonoBehaviour
{
  private PhotonView canvasPhotonView = null;
  void Start()
  {
    canvasPhotonView = GetComponent<PhotonView>();
  }
  public void OnClick_ResetRoundLock()
  {
    if (!GameManager.globalManager.isOnlineMode) return;
    // The master should tell alien to unlock round saving
    if (PhotonNetwork.IsMasterClient)
    {
      GameManager.globalManager.isRoundSaved = false;
      canvasPhotonView.RPC("RPC_ResetRoundLock", RpcTarget.Others);
    }
  }

  [PunRPC]
  void RPC_ResetRoundLock()
  {
    GameManager.globalManager.isRoundSaved = false;
  }
}
