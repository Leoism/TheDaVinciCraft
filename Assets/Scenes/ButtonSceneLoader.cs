using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class ButtonSceneLoader : MonoBehaviour
{
  void Start() { }

  public void LoadScene(string scene)
  {
    if (GameManager.globalManager.isOnlineMode)
    {
      PhotonNetwork.LoadLevel(scene);
    }
    else
    {
      SceneManager.LoadScene(scene);
    }
  }
}
