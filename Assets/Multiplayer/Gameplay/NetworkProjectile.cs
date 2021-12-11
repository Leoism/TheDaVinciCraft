using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class NetworkProjectile : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
  public void OnPhotonInstantiate(PhotonMessageInfo info)
  {
    GameObject photonGameObj = info.photonView.gameObject;
    string projectileName = (string)info.photonView.InstantiationData[0];
    photonGameObj.GetComponent<SpriteRenderer>().sprite = GetProjectileSpritePrefab(projectileName).sprite;
    // Only interactions should happen on the alien side
    if (!projectileName.Equals("Boomerang") && info.photonView.IsMine)
      photonGameObj.AddComponent<WeaponInteractions>();
    // the human should not affect anything, it should only spectate
    if (!info.photonView.IsMine)
    {
      Destroy(photonGameObj.GetComponent<BoxCollider2D>());
      Destroy(photonGameObj.GetComponent<Rigidbody2D>());
    }
  }

  private SpriteRenderer GetProjectileSpritePrefab(string name)
  {
    GameObject spritePrefab = null;
    switch (name)
    {
      case "Alien Grenade ":
        spritePrefab = (GameObject)Resources.Load("Weapons/AlienGrenade");
        break;
      case "Arrow":
        spritePrefab = (GameObject)Resources.Load("Weapons/Arrow");
        break;
      case "Bomb ":
        spritePrefab = (GameObject)Resources.Load("Weapons/Bomb");
        break;
      case "Boomerang":
        spritePrefab = (GameObject)Resources.Load("Weapons/Boomerang");
        break;
      case "Bowling Ball ":
        spritePrefab = (GameObject)Resources.Load("Weapons/Ball");
        break;
      case "Deforestor":
        spritePrefab = (GameObject)Resources.Load("Weapons/Deforestor");
        break;
      case "Magnet":
        spritePrefab = (GameObject)Resources.Load("Weapons/Magnet");
        break;
      case "Mineral Extractor":
        spritePrefab = (GameObject)Resources.Load("Weapons/MineralExtractor");
        break;
      case "Raygun ":
        spritePrefab = (GameObject)Resources.Load("Weapons/Ray");
        break;
      default:
        spritePrefab = null;
        break;
    }
    return spritePrefab.GetComponent<SpriteRenderer>();
  }
}
