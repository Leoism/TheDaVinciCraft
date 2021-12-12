using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class RayBehavior : MonoBehaviour
{
  private Vector3 targetPosition;
  private float speed = 120f;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.smoothDeltaTime);
    transform.up = (targetPosition - transform.position).normalized;
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
    string shatterType = collision.gameObject.tag;
    // The Ray should not pass through glass
    if (shatterType.Contains("GlassTile"))
    {
      if (GameManager.globalManager.isOnlineMode)
      {
        // Only the alien should destroy projectiles
        if (!PhotonNetwork.IsMasterClient)
          PhotonNetwork.Destroy(GetComponent<PhotonView>());
      }
      else
      {
        Destroy(gameObject);
      }
    }
  }
}
