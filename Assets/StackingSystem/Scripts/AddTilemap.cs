using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

namespace StackingSystem.Scripts
{
    // Will attach to the Grid
    public class AddTilemap : MonoBehaviour
    {
        [SerializeField] private TileBase[] tilesToAdd;
        [SerializeField] private int currentTileToAddIndex;
        [SerializeField] private Tilemap tilemapPrefab;
        [SerializeField] private Tilemap tempTilemap;
        [SerializeField] private Text indexText;

        private TileBase CurrentTileToAdd => tilesToAdd[currentTileToAddIndex];
        private int CurrentTileToAddIndex
        {
            get => currentTileToAddIndex;
            set
            {
                currentTileToAddIndex = value;
                indexText.text = "" + value;
            }
        }

        private Camera _mainCamera;
        private Grid _grid;

        private void Start()
        {
            _mainCamera = Camera.main;
            _grid = GetComponent<Grid>();
        }

        private void Update()
        {
            HandleTileMapCreation();
            HandleTileAddSwitch();
        }

        private readonly HashSet<Vector3Int> _addedTiles = new HashSet<Vector3Int>();

        private void HandleTileMapCreation()
        {
            if (Input.GetMouseButtonUp(0))
            {
                // Creates a new tilemap from the prefab as a child of this
                var tilemap = Instantiate(tilemapPrefab, transform);

                foreach (var pos in _addedTiles)
                    tilemap.SetTile(pos, CurrentTileToAdd);

                _addedTiles.Clear();
                tempTilemap.ClearAllTiles();
            }

            if (Input.GetMouseButton(0))
            {
                var mousePos = _mainCamera.ScreenToWorldPoint(Input.mousePosition);
                var newTilePos = _grid.WorldToCell(mousePos);

                // If none of the tiles are adjacent, then do not place newTilePos
                if (_addedTiles.Count != 0 &&
                    _addedTiles.All(pos => pos.ManhattanDistance(newTilePos) != 1)) return;

                _addedTiles.Add(newTilePos);
                tempTilemap.SetTile(newTilePos, CurrentTileToAdd);
            }
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
    }
}