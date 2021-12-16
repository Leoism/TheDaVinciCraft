using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class GrappleBehavior : MonoBehaviour
{
  private Vector3 targetPosition;
  private float speed = 120f;
  // Start is called before the first frame update
  void Start()
  {
    transform.localScale = new Vector3(3, 3, 1);
    transform.eulerAngles -= new Vector3(0, 0, 60);
  }

  // Update is called once per frame
  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.smoothDeltaTime);
    if (transform.position == targetPosition)
    {
      if (GameManager.globalManager.isOnlineMode)
      {
        if (GetComponent<PhotonView>().IsMine)
          PhotonNetwork.Destroy(GetComponent<PhotonView>());
      }
      else
      {
        Destroy(gameObject);
      }
    }
  }

  public void SetTarget(Vector3 target)
  {
    targetPosition = (Vector2)target;
  }

  public void OnCollisionEnter2D(Collision2D collision)
  {
    if (GameManager.globalManager.isOnlineMode)
    {
      PhotonView.Find(2).RPC("RPC_StopAudio", RpcTarget.MasterClient);
      PhotonNetwork.Destroy(gameObject.GetPhotonView());
    }
    else
    {
      Destroy(gameObject);
    }
  }
}
