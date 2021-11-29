using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;
public class NetworkTilemap : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
  public void OnPhotonInstantiate(PhotonMessageInfo info)
  {
    Tilemap currentTileMap = info.photonView.gameObject.GetComponent<Tilemap>();
    Split splitBehavior = currentTileMap.GetComponent<Split>();
    splitBehavior.CurrentTileToAdd = (TileBase)Resources.Load("Seamless-Okawood-Texture");
    object data = info.photonView.InstantiationData[0];
    Vector3Int[] _addedTiles = (Vector3Int[])data;

    foreach (Vector3Int pos in _addedTiles)
    {
      Debug.Log(pos);
      currentTileMap.SetTile(pos, splitBehavior.CurrentTileToAdd);
    }
  }
}
