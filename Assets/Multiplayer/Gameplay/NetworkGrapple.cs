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
    if (PhotonNetwork.IsMasterClient)
    {
      transform.localScale = new Vector3(3, 3, 1);
      transform.eulerAngles -= new Vector3(0, 0, 60);
      photonGameObj.tag = "";
      Destroy(photonGameObj.GetComponent<PolygonCollider2D>());
    }
  }
}
