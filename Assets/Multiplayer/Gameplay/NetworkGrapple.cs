using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkGrapple : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
  public void OnPhotonInstantiate(PhotonMessageInfo info)
  {
    GameObject photonGameObj = info.photonView.gameObject;
    if (!PhotonNetwork.IsMasterClient)
      photonGameObj.AddComponent<GrappleBehavior>().SetTarget((Vector3)info.photonView.InstantiationData[0]);
    if (!info.photonView.IsMine)
    {
      Destroy(photonGameObj.GetComponent<BoxCollider2D>());
    }
  }
}
