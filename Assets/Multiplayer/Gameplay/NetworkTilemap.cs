using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using Photon.Pun;
public class NetworkTilemap : MonoBehaviourPunCallbacks, IPunInstantiateMagicCallback
{
  void Update()
  {
    if (!GameManager.globalManager.isOnlineMode) return;
    Rigidbody2D rb = gameObject.GetComponent<Rigidbody2D>();
    TilemapCollider2D bc = gameObject.GetComponent<TilemapCollider2D>();

    if (gameObject.GetPhotonView().IsMine)
    {
      if (rb == null && bc == null)
      {
        gameObject.AddComponent<Rigidbody2D>();
        gameObject.AddComponent<TilemapCollider2D>();
      }
    }
    else
    {
      if (rb != null && bc != null)
      {
        Destroy(rb);
        Destroy(bc);
      }
    }
  }
  public void OnPhotonInstantiate(PhotonMessageInfo info)
  {
    GameObject networkPhotonGameObj = info.photonView.gameObject;
    if (info.photonView.InstantiationData.Length > 2)
    {
      int viewID = (int)info.photonView.InstantiationData[2];
      Vector3 pos = (Vector3)info.photonView.InstantiationData[3];
      Quaternion rot = (Quaternion)info.photonView.InstantiationData[4];
      Vector3 scale = (Vector3)info.photonView.InstantiationData[5];
      networkPhotonGameObj.transform.parent = PhotonView.Find(viewID).gameObject.transform;
      networkPhotonGameObj.transform.position = pos;
      networkPhotonGameObj.transform.rotation = rot;
      networkPhotonGameObj.transform.localScale = scale;
    }
    else
    {
      GameObject gridGameObj = GameObject.FindGameObjectWithTag("Grid");
      networkPhotonGameObj.transform.parent = gridGameObj.transform;
      networkPhotonGameObj.transform.position = gridGameObj.transform.position;
      networkPhotonGameObj.transform.rotation = gridGameObj.transform.rotation;
      networkPhotonGameObj.transform.localScale = gridGameObj.transform.localScale;
    }

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
