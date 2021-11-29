using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
public class Split : MonoBehaviour
{
    public TileBase CurrentTileToAdd;
    public GameObject shatter;
    private Tilemap tl;
    private Tile t;
    private ShatterableBehavior sb;
    private bool create = false;
    private Vector3Int oldTilePos;
    [SerializeField] private Tilemap tilemapPrefab;
    private readonly HashSet<Vector3Int> _addedTiles = new HashSet<Vector3Int>();
    // Start is called before the first frame update
    void Start()
    {
        tl = GetComponent<Tilemap>();    
        for (int x = tl.cellBounds.min.x; x < tl.cellBounds.max.x; x++)
        {
            for (int y = tl.cellBounds.min.y; y < tl.cellBounds.max.y; y++)
            {
                if (tl.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    String shatterType = tl.GetTile<Tile>(new Vector3Int(x, y, 0)).sprite.ToString();
                    shatterType = shatterType.Substring(0, 1);
                    shatterType += "Shatter";
                    Debug.Log(shatterType);
                    shatter = Resources.Load(shatterType) as GameObject;
                    shatter.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    Debug.Log(shatter.name);
                    return;
                    /*Debug.Log(tl.GetTile<Tile>(new Vector3Int(x, y, 0)).sprite);
                    shatter.GetComponent<SpriteRenderer>().sprite = tl.GetTile<Tile>(new Vector3Int(x, y, 0)).sprite;*/
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        _addedTiles.Clear();
        bool breaking = true;

        if (collision.gameObject.CompareTag("Weapon"))
        {
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            Vector3 hitPosition = Vector3.zero;
            int tilesDestroyed = 0;
            List<Vector3Int> hitPos = new List<Vector3Int>();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                hitPosition.x = hit.point.x + 2f * hit.normal.x;
                hitPosition.y = hit.point.y + 2f * hit.normal.y;

                t = tl.GetTile<Tile>(tl.WorldToCell(hitPosition));
               // Debug.Log(t.sprite);
                if (tl.GetTile(tl.WorldToCell(hitPosition)) != null)
                {
                    tl.SetTile(tl.WorldToCell(hitPosition), null);
                    hitPos.Add(tl.WorldToCell(hitPosition));
                  
                    tilesDestroyed++;
                }
              
            }
            if (t != null)
            {
                //shatter.GetComponent<SpriteRenderer>().sprite = t.sprite;
            }
                  for (int i = 0; i <= tilesDestroyed - 1; i++)
            {
                Vector3 shattterPos = tl.GetCellCenterWorld(hitPos[i]);
                // GameObject s = Instantiate(shatter, shattterPos, Quaternion.identity);
                for (int z = 0; z < 3; z++)
                {
                    shattterPos = tl.GetCellCenterWorld(hitPos[i]);
                    switch (z)
                    {
                        case 0:
                            shattterPos.y = shattterPos.y + 0.5f;
                            shattterPos.x = shattterPos.x - 0.5f;
                            Instantiate(shatter, shattterPos, Quaternion.identity);
                            break;
                        case 1:
                            shattterPos.y = shattterPos.y + 0.5f;
                            shattterPos.x = shattterPos.x + 0.5f;
                            Instantiate(shatter, shattterPos, Quaternion.identity);
                            break;
                        case 2:
                            shattterPos.y = shattterPos.y - 0.5f;
                            shattterPos.x = shattterPos.x - 0.5f;
                            Instantiate(shatter, shattterPos, Quaternion.identity);
                            break;
                        case 3:
                            shattterPos.y = shattterPos.y - 0.5f;
                            shattterPos.x = shattterPos.x + 0.5f;
                            Instantiate(shatter, shattterPos, Quaternion.identity);
                            break;
                    }
                    
                }
                    
             /*   sb = s.GetComponent<ShatterableBehavior>();
                sb.sr = s.GetComponent<SpriteRenderer>();
                sb.triggerShatter();*/
            }

            Destroy(collision.gameObject);
            for (int x = tl.cellBounds.min.x; x < tl.cellBounds.max.x && breaking; x++)
            {
                for (int y = tl.cellBounds.min.y; y < tl.cellBounds.max.y && breaking; y++)
                {
                    if (tl.GetTile(new Vector3Int(x, y, 0)) != null)
                    {
                        if (_addedTiles.Count != 0 &&
                              _addedTiles.All(pos => ManhattanDistance(oldTilePos, new Vector3Int(x, y, 0)) != 1))
                        {
                            breaking = false;
                            foreach (var pos in _addedTiles)
                            {
                                tl.SetTile(pos, null);
                            }
                            create = true;
                        }
                        else
                        {
                            oldTilePos = new Vector3Int(x, y, 0);
                            _addedTiles.Add(new Vector3Int(x, y, 0));
                        }

                    }

                }
            }
        }
        if (create)
            createTileMap();
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    public int ManhattanDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
    }

    private void createTileMap()
    {
        var tilemap = Instantiate(tilemapPrefab, transform.parent);
        tilemap.ClearAllTiles();
        foreach (var pos in _addedTiles)
            tilemap.SetTile(pos, CurrentTileToAdd);
        _addedTiles.Clear();
        create = false;
    }
}