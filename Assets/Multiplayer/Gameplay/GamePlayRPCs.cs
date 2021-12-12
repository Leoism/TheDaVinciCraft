using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;
public class GamePlayRPCs : MonoBehaviourPunCallbacks
{
  [SerializeField]
  private Timer timer;
  [SerializeField]
  private NetworkSFXPlayer networkSFXPlayer;
  [PunRPC]
  void RPC_OnArtDestruction()
  {
    PhotonNetwork.LoadLevel("AlienWin");
  }

  [PunRPC]
  void RPC_OnClick_Done()
  {
    timer.finishState();
  }

  [PunRPC]
  void RPC_AlienOutOfWeapons()
  {
    PhotonNetwork.LoadLevel("HumanWin");
  }

  [PunRPC]
  void RPC_OnTilemapHit(Vector3Int[] tilePositionsToDestory, Vector3Int[] _addedTiles, int viewID)
  {
    Tilemap targetTilemap = PhotonView.Find(viewID).gameObject.GetComponent<Tilemap>();
    foreach (Vector3Int pos in tilePositionsToDestory)
    {
      targetTilemap.SetTile(pos, null);
    }
  }

  [PunRPC]
  void RPC_OnCollisionPlayExplosion(int viewID, string name)
  {
    GameObject photonGameObj = PhotonView.Find(viewID).gameObject;
    if (name == "Ray")
    {
      photonGameObj.GetComponentInChildren<ParticleSystem>().Play();
      photonGameObj.GetComponent<ProjectileSFXHandler>().PlayClipAtIndex(0);
      return;
    }
    photonGameObj.GetComponent<ParticleSystemHandler>().PlayByName(name);
    photonGameObj.GetComponent<ProjectileSFXHandler>().PlayClipByName(name);
  }

  [PunRPC]
  void RPC_PlayBoomerang()
  {
    networkSFXPlayer.PlayClip("Boomerang");
  }

  [PunRPC]
  void RPC_PlayRay()
  {
    networkSFXPlayer.PlayClip("Ray");
  }
   [PunRPC]
  void RPC_PlayGrapple()
  {
    networkSFXPlayer.PlayClip("Boomerang");
  }

  [PunRPC]
  void RPC_PlayCatapult()
  {
    networkSFXPlayer.PlayClip("Catapult");
  }

  [PunRPC]
  void RPC_StopAudio()
  {
    networkSFXPlayer.Stop();
  }

  /*
 * Positive distance between two vector points
 */
  private int ManhattanDistance(Vector3Int a, Vector3Int b)
  {
    return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
  }
}
