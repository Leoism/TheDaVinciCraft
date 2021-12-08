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
  [SerializeField]
  private GameObject waitingMenu;
  public void OnClick_PlayerReady()
  {
    if (!GameManager.globalManager.isOnlineMode)
    {
      RunGameObjectChanges();
      return;
    }
    // sendRPC, but only if you're an alien
    if (!PhotonNetwork.IsMasterClient)
    {
      DeactivateGameObjects();
      waitingMenu.SetActive(true);
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
    if (!GameManager.globalManager.allPlayersReady)
    {
      DeactivateGameObjects();
      if (!waitingMenu.activeSelf)
      {
        waitingMenu.SetActive(true);
      }
      return;
    }
    RunGameObjectChanges();
    // send the selected art
    canvasPhotonView.RPC("RPC_SendSelectedArt", RpcTarget.Others, PlayerPrefs.GetInt("selectedArt"));
  }

  public void OnWait_CheckOtherPlayerReady()
  {
    if (!PhotonNetwork.IsMasterClient) return;
    OnClick_CheckOtherPlayerReady();
  }

  private void RunGameObjectChanges()
  {
    ActivateGameObjects();
    DeactivateGameObjects();
  }

  private void ActivateGameObjects()
  {
    foreach (GameObject go in gameObjectsToEnable)
    {
      go.SetActive(true);
    }
  }

  private void DeactivateGameObjects()
  {
    foreach (GameObject go in gameObjectsToDisable)
    {
      go.SetActive(false);
    }
  }
}
