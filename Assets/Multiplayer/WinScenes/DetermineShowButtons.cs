using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class DetermineShowButtons : MonoBehaviour
{
  // Start is called before the first frame update
  void Awake()
  {
    // do not show for the alien
    if (GameManager.globalManager.isOnlineMode && !PhotonNetwork.IsMasterClient)
    {
      gameObject.SetActive(false);
    }
  }
}
