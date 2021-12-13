using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkShatter : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
  public void OnPhotonInstantiate(PhotonMessageInfo info)
  {
    GameObject photonGameObj = info.photonView.gameObject;
    photonGameObj.tag = (string)info.photonView.InstantiationData[0];
    // the human should not affect anything, it should only spectate
    if (!info.photonView.IsMine)
    {
      Destroy(photonGameObj.GetComponent<BoxCollider2D>());
      Destroy(photonGameObj.GetComponent<Rigidbody2D>());
    }
  }
}
