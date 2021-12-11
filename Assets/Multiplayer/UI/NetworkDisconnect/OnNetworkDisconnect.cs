using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class OnNetworkDisconnect : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    if (GameManager.globalManager.isOnlineMode && PhotonNetwork.IsConnected)
    {
      PhotonNetwork.Disconnect();
    }
  }
}
