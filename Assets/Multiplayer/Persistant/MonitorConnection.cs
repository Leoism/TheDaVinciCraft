using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;

public class MonitorConnection : MonoBehaviourPunCallbacks
{
  // Start is called before the first frame update
  void Start()
  {
    DontDestroyOnLoad(gameObject);
  }

  public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
  {
    SceneManager.MoveGameObjectToScene(gameObject, SceneManager.GetActiveScene());
    SceneManager.LoadScene("NetworkDisconnected");
  }
}
