using System;
using System.Collections;
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
        tl = GetComponent<Tilemap>(); //Reference to tilemap
        /*
         * Goes through tilemap until it encounters a non-null tile, grabs that tile type
         * and sets the shatter type accordingly. I.e. if tilemap is wood, shatter will be wood.
        */
        for (int x = tl.cellBounds.min.x; x < tl.cellBounds.max.x; x++)
        {
            for (int y = tl.cellBounds.min.y; y < tl.cellBounds.max.y; y++)
            {
                if (tl.GetTile(new Vector3Int(x, y, 0)) != null)
                {
                    String shatterType = tl.GetTile<Tile>(new Vector3Int(x, y, 0)).sprite.name;
                    shatterType += "Tile";
                    shatter = Resources.Load(shatterType) as GameObject;
                    shatter.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
                    return; // Returns upon finding a non-nulltile
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Resets added tile hashset so that it can be used later to track split tilemaps
        _addedTiles.Clear();
        //Variable tracking whether the tilemap is continous (ie is there a big break between tiles)
        //Counterintuatively, true means there is not
        bool breaking = true;
        //When hit by weapon projectile
        if (collision.gameObject.CompareTag("Weapon"))
        {
            //Gets the projectile type using the sprite attatched to it
            string projType = collision.gameObject.GetComponent<SpriteRenderer>().sprite.name.ToString();
            //Debug.Log(projType);
            /*
             * Switch statement which allows material-weapon interaction
             * In this, it only destroys the weapon if it hits a type it can destroy otherwise it bounces off
             * and destroys after 3 seconds (waiter subroutine)
             */
            switch (projType)
            {
                case ("deforestor"):
                    if (shatter.name != "WoodTile" && shatter.name != "FabricTile")
                    {
                        //StartCoroutine(waiter(collision.gameObject));
                        return;
                    }
                    break;
                case ("Ball"):
                    if (shatter.name != "GlassTile" && shatter.name != "StoneTile")
                    {
                        //StartCoroutine(waiter(collision.gameObject));
                        return;
                    }
                    break;
                case ("Arrow"):
                    if (shatter.name != "FabricTile")
                    {
                        //StartCoroutine(waiter(collision.gameObject));
                        return;
                    }
                    break;
                case ("Magnet"):
                    if (shatter.name != "MetalTile" && shatter.name != "FabricTile")
                    {
                        // StartCoroutine(waiter(collision.gameObject));
                        return;
                    }
                    break;
                case ("Boomerange"):
                    if (shatter.name != "GlassTile" && shatter.name != "FabricTile")
                    {
                        // StartCoroutine(waiter(collision.gameObject));

                        return;
                    }
                    break;
                case ("MineralExtractor"):
                    if (shatter.name != "StoneTile" && shatter.name != "FabricTile")
                    {
                        //StartCoroutine(waiter(collision.gameObject));  
                        return;
                    }
                    break;
                // Ray uses a square sprite
                case ("Square"):
                    if (shatter.name == "GlassTile")
                    {
                        //StartCoroutine(waiter(collision.gameObject));  
                        return;
                    }
                    break;

            }
            //Make the rigidbody of the tilemap static so that it does not move on hit
            GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
            //Vector tracking hit position on the tilemap
            Vector3 hitPosition = Vector3.zero;
            //Num tile destroyed by hit
            int tilesDestroyed = 0;
            //A list containing all the tiles hit in tilemap space
            List<Vector3Int> hitPos = new List<Vector3Int>();
            foreach (ContactPoint2D hit in collision.contacts)
            {
                //Hit point is offset so that it refers to center of cell rather than edge
                hitPosition.x = hit.point.x + 2f * hit.normal.x;
                hitPosition.y = hit.point.y + 2f * hit.normal.y;
                //Get the tile at the hitpoint
                t = tl.GetTile<Tile>(tl.WorldToCell(hitPosition));
                //If there is a tile placed tile there
                if (tl.GetTile(tl.WorldToCell(hitPosition)) != null)
                {
                    //Clear that tile
                    tl.SetTile(tl.WorldToCell(hitPosition), null);
                    //Add that tile to the hit tiles list
                    hitPos.Add(tl.WorldToCell(hitPosition));
                    //Up the num tiles destroyed
                    tilesDestroyed++;
                }

            }
            //Goes through the tiles destroyed and applies shatter
            for (int i = 0; i <= tilesDestroyed - 1; i++)
            {
                Vector3 shattterPos = tl.GetCellCenterWorld(hitPos[i]);
                //Creates three smaller blocks that have the same sprite as tile destroyed at the hit pos
                //Offsets each shatter spawn a small amount so that one shatter spawns per quadrant
                for (int z = 0; z < 4; z++)
                {
                    // resets to cell world center
                    shattterPos = tl.GetCellCenterWorld(hitPos[i]);
                    switch (z)
                    {
                        case 0:
                            shattterPos.y = shattterPos.y + 0.5f;
                            shattterPos.x = shattterPos.x - 0.5f;
                            break;
                        case 1:
                            shattterPos.y = shattterPos.y + 0.5f;
                            shattterPos.x = shattterPos.x + 0.5f;
                            break;
                        case 2:
                            shattterPos.y = shattterPos.y - 0.5f;
                            shattterPos.x = shattterPos.x - 0.5f;
                            break;
                        case 3:
                            shattterPos.y = shattterPos.y - 0.5f;
                            shattterPos.x = shattterPos.x + 0.5f;
                            break;
                    }
                    GameObject shatterTile = Instantiate(shatter, shattterPos, Quaternion.identity) as GameObject;
                    shatterTile.AddComponent<shatterDestroy>();
                }
            }
            /*
             * Goes through tilemap, searches for non-continous portions.
             * When found, it saves the tiles left, destroyes the tilemap, and recreates it as 2 seperate tilemaps
             */
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
        // If there has been a break, create new tilemaps
        if (create)
            createTileMap();
        //Reset rigidbody type so that it can interact with world again
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }
    /*
     * Positive distance between two vector points
     */
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
    IEnumerator waiter(GameObject collision)
    {
        //Wait for 3 seconds
        yield return new WaitForSeconds(3);
        Destroy(collision);
    }
}