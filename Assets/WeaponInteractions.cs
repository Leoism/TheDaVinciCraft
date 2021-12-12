using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class WeaponInteractions : MonoBehaviour
{
  bool isCoroutineRunning = false;
  // Start is called before the first frame updat

  // Update is called once per frame
  void Update()
  {
    float speed = Mathf.Round(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
    if (speed == 0)
    {
      StartCoroutine(waiter(gameObject));
    }
    else if (speed > 0 && isCoroutineRunning)
    {
      StopCoroutine(waiter(gameObject));
      isCoroutineRunning = false;
    }
  }

  IEnumerator waiter(GameObject collision)
  {
    isCoroutineRunning = true;
    //Wait for 1.5 seconds
    yield return new WaitForSeconds(1.5f);
    if (isCoroutineRunning)
    {
      if (GameManager.globalManager.isOnlineMode)
      {
        PhotonNetwork.Destroy(collision.GetPhotonView());
      }
      else
      {
        Destroy(collision);
      }
    }
  }

  void OnCollisionEnter2D(Collision2D collision)
  {
    ParticleSystemHandler particleSystemHandler = GetComponent<ParticleSystemHandler>();
    ParticleSystem particleSystem = null;
    string name = "";
    if (GetComponent<SpriteRenderer>().sprite.name == "bumb")
    {
      Debug.Log("boomb");
      particleSystem = particleSystemHandler.PlayByName("Bomb");
      GetComponent<ProjectileSFXHandler>().PlayClipByName("Bomb");
      name = "Bomb";
    }
    else if (GetComponent<SpriteRenderer>().sprite.name == "alien grenade")
    {
      particleSystem = particleSystemHandler.PlayByName("Alien Grenade");
      GetComponent<ProjectileSFXHandler>().PlayClipByName("Bomb");
      name = "Alien Grenade";
    }

    if (name == "") return;

    if (GameManager.globalManager.isOnlineMode)
    {
      PhotonView.Find(2)/* canvas photon view*/.RPC("RPC_OnCollisionPlayExplosion", RpcTarget.Others, new object[] { gameObject.GetPhotonView().ViewID, name });
      StartCoroutine(WaitPhotonDestroy(particleSystem.main.duration));
    }
    else
    {
      Destroy(gameObject, particleSystem.main.duration);
    }
  }

  IEnumerator WaitPhotonDestroy(float time)
  {
    yield return new WaitForSeconds(time);
    PhotonNetwork.Destroy(gameObject.GetPhotonView());
  }
}
