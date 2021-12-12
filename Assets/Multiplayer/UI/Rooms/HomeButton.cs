using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
public class HomeButton : MonoBehaviour
{
  public void OnClick_DisconnectFromServer()
  {
    if (PhotonNetwork.IsConnected)
    {
      PhotonNetwork.Disconnect();
      SceneManager.LoadScene("GameMenu");
    }
  }
}
