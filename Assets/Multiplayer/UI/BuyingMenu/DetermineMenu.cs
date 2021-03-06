using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DetermineMenu : MonoBehaviour
{
  [SerializeField]
  private GameObject alienMessage;
  [SerializeField]
  private GameObject humanMessage;
  // Start is called before the first frame update
  void Start()
  {
    if (!GameManager.globalManager.isOnlineMode)
    {
      alienMessage.SetActive(true);
      return;
    }
    if (PhotonNetwork.IsMasterClient)
    {
      humanMessage.SetActive(true);
      GameManager.globalManager.SetAlienInventory(new List<GameObject>());
    }
    else
    {
      alienMessage.SetActive(true);
      GameManager.globalManager.SetHumanInventory(new List<GameObject>());
    }
  }

  [PunRPC]
  // received
  private void RPC_PlayerFinishedBuying()
  {
    GameManager.globalManager.allPlayersReady = true;
  }

  [PunRPC]
  private void RPC_SendSelectedArt(int artIdx)
  {
    PlayerPrefs.SetInt("selectedArt", artIdx);
  }
}
