using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;
public class NetworkTilemap : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
  public void OnPhotonInstantiate(PhotonMessageInfo info)
  {
    GameObject gridGameObj = GameObject.FindGameObjectWithTag("Grid");
    GameObject networkPhotonGameObj = info.photonView.gameObject;
    networkPhotonGameObj.transform.parent = gridGameObj.transform;
    networkPhotonGameObj.transform.position = gridGameObj.transform.position;
    networkPhotonGameObj.transform.rotation = gridGameObj.transform.rotation;
    networkPhotonGameObj.transform.localScale = gridGameObj.transform.localScale;

    Tilemap currentTileMap = networkPhotonGameObj.GetComponent<Tilemap>();
    currentTileMap.ClearAllTiles();
    string tileSpriteName = (string)info.photonView.InstantiationData[1];
    currentTileMap.tag = tileSpriteName + "Tile";
    Tile newTile = new Tile();
    newTile.sprite = GetTileSpritePrefab(tileSpriteName).sprite;
    Split splitBehavior = currentTileMap.GetComponent<Split>();
    splitBehavior.CurrentTileToAdd = newTile;
    object data = info.photonView.InstantiationData[0];
    Vector3Int[] _addedTiles = (Vector3Int[])data;

    foreach (Vector3Int pos in _addedTiles)
    {
      currentTileMap.SetTile(pos, splitBehavior.CurrentTileToAdd);
    }
  }

  private SpriteRenderer GetTileSpritePrefab(string name)
  {
    GameObject spritePrefab = null;
    switch (name)
    {
      case "Fabric":
        spritePrefab = (GameObject)Resources.Load("FabricTile");
        break;
      case "Metal":
        spritePrefab = (GameObject)Resources.Load("MetalTile");
        break;
      case "Glass":
        spritePrefab = (GameObject)Resources.Load("GlassTile");
        break;
      case "Stone":
        spritePrefab = (GameObject)Resources.Load("StoneTile");
        break;
      case "Wood":
        spritePrefab = (GameObject)Resources.Load("WoodTile");
        break;
      default:
        spritePrefab = null;
        break;
    }

    return spritePrefab.GetComponent<SpriteRenderer>();
  }
}
