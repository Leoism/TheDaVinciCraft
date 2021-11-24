using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OtherPlayerStatus : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private GameObject[] gameObjectsToDisable;
  [SerializeField]
  private GameObject[] gameObjectsToEnable;
  [SerializeField]
  private PhotonView canvasPhotonView;
  public void OnClick_PlayerReady()
  {
    if (!GameManager.globalManager.isOnlineMode || PhotonNetwork.IsMasterClient)
    {
      RunGameObjectChanges();
      return;
    }
    // sendRPC, but only if you're an alien
    if (!PhotonNetwork.IsMasterClient)
    {
      canvasPhotonView.RPC("RPC_PlayerFinishedBuying", RpcTarget.MasterClient);
    }
  }

  public void OnClick_CheckOtherPlayerReady()
  {
    if (!GameManager.globalManager.isOnlineMode)
    {
      RunGameObjectChanges();
      return;
    }
    // if the other player is not ready, do not proceed
    if (!GameManager.globalManager.allPlayersReady) return;
    RunGameObjectChanges();
    // send the selected art
    canvasPhotonView.RPC("RPC_SendSelectedArt", RpcTarget.Others, PlayerPrefs.GetInt("selectedArt"));
  }

  private void RunGameObjectChanges()
  {
    foreach (GameObject go in gameObjectsToEnable)
    {
      go.SetActive(true);
    }
    foreach (GameObject go in gameObjectsToDisable)
    {
      go.SetActive(false);
    }
  }
}
