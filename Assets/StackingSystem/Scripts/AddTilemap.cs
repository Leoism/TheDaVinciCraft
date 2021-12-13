using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using Photon.Pun;
using ExitGames.Client.Photon;
/*
namespace StackingSystem.Scripts
{*/
// Will attach to the Grid
public class AddTilemap : MonoBehaviour
{
    public Gameplay gameplayScene = null;
    [SerializeField] private TileBase[] tilesToAdd;
    [SerializeField] private int currentTileToAddIndex;
    [SerializeField] private Tilemap tilemapPrefab;
    [SerializeField] private Tilemap tempTilemap;
    public TileBase tileType;
    private bool xDom;
    private bool yDom;
    private bool placed;
    private Vector3Int oldTilePos;
    //[SerializeField] private Text indexText;

    public TileBase CurrentTileToAdd => tilesToAdd[currentTileToAddIndex];
    private int CurrentTileToAddIndex
    {
        get => currentTileToAddIndex;
        set
        {
            currentTileToAddIndex = value;
            //indexText.text = "" + value;
        }
    }

    private Camera _mainCamera;
    private Grid _grid;

    private void Start()
    {
        placed = false;
        xDom = false;
        yDom = false;
        _mainCamera = Camera.main;
        _grid = GetComponent<Grid>();
    }

    private void Update()
    {
        HandleTileMapCreation();
        HandleTileAddSwitch();
    }

    public void UpdateTileArt(Tile newTile)
    {
        tilesToAdd[currentTileToAddIndex] = newTile;
        if (tilesToAdd[currentTileToAddIndex])
            tilesToAdd[currentTileToAddIndex].name = newTile ? newTile.name : "None";
    }

    private HashSet<Vector3Int> _addedTiles = new HashSet<Vector3Int>();

    private void HandleTileMapCreation()
    {
        if (CurrentTileToAdd == null) return;

        Vector2 ray = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        RaycastHit2D hit = Physics2D.Raycast(ray, Vector2.zero);
        if (hit.collider && hit.collider.CompareTag("Art")) return;
        
        if (Input.GetMouseButtonUp(0))
        {
            CreateTileMap();
        }
        if (Input.GetMouseButton(0) && GameManager.globalManager.humanInventory.TotalCount() != 0)
        {
            if (EventSystem.current.IsPointerOverGameObject ())
                return;
            var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
            var newTilePos = _grid.WorldToCell(mousePos);
            // If none of the tiles are adjacent, then do not place newTilePos
            if (_addedTiles.Count != 0 &&
                _addedTiles.All(pos => ManhattanDistance(oldTilePos, newTilePos) != 1)) return;
            
            if (placed)
            {
                if (xDom && oldTilePos.y - newTilePos.y == 0)
                    return;
                else if (yDom && oldTilePos.x - newTilePos.x == 0)
                    return;

                if (oldTilePos.y - newTilePos.y != 0)
                    xDom = true;
                else
                    yDom = true;
            }
                  
            placed = true;
            if (!_addedTiles.Contains(newTilePos)) {
                _addedTiles.Add(newTilePos);
                 oldTilePos = newTilePos;
                tempTilemap.SetTile(newTilePos, CurrentTileToAdd);
                gameplayScene.UseItem();
            }
            
        }
    }

    public void CreateTileMap()
    {
        // Creates a new tilemap from the prefab as a child of this
        if (GameManager.globalManager.isOnlineMode)
        {
            Vector3Int[] v3iArr = new Vector3Int[_addedTiles.Count];
            _addedTiles.CopyTo(v3iArr);
            PhotonNetwork.Instantiate("Tilemap_Build", transform.position, transform.rotation, 0, new object[] {v3iArr, CurrentTileToAdd.name});
            _addedTiles.Clear();
            tempTilemap.ClearAllTiles();
            placed = false;
            xDom = false;
            yDom = false;
        }
        else
        {
            Tilemap newTilemap = Instantiate(((GameObject)Resources.Load("Tilemap_Build"))).GetComponent<Tilemap>();
            // if you don't clear them, then the tilemap gets prebuilt tiles
            newTilemap.ClearAllTiles();
            newTilemap.transform.position = transform.position;
            newTilemap.transform.rotation = transform.rotation;
            newTilemap.transform.localScale = transform.localScale;
            newTilemap.transform.parent = transform;
            SetTileMap(newTilemap);
        }
    }

    private void SetTileMap(Tilemap tilemap)
    {
        tilemap.GetComponent<Split>().CurrentTileToAdd = CurrentTileToAdd;
        foreach (var pos in _addedTiles)
            tilemap.SetTile(pos, CurrentTileToAdd);
        tilemap.tag = CurrentTileToAdd.name + "Tile";
        _addedTiles.Clear();
        tempTilemap.ClearAllTiles();
        placed = false;
        xDom = false;
        yDom = false;
    }


    private void HandleTileAddSwitch()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            CurrentTileToAddIndex = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CurrentTileToAddIndex = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            CurrentTileToAddIndex = 2;
        }
    }

    public int ManhattanDistance(Vector3Int a, Vector3Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y) + Mathf.Abs(a.z - b.z);
    }
}
//}