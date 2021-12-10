using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkRay : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
  public void OnPhotonInstantiate(PhotonMessageInfo info)
  {
    GameObject photonGameObj = info.photonView.gameObject;
    photonGameObj.AddComponent<RayBehavior>().SetTarget((Vector3)info.photonView.InstantiationData[0]);
  }
}
