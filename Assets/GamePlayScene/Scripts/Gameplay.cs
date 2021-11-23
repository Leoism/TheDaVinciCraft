using System;
using System.Collections.Generic;
using GamePlayScene.ScriptableObjects.MaterialsScriptableObject;
using GamePlayScene.ScriptableObjects.WeaponsScriptableObject;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public WeaponStore weaponStore;
    public Timer gameTimer;
    public Camera mainCamera;
    public Transform buildingZone;
    public Transform alienZone;
    public Alienship alienship;
    public Canvas mainCanvas;
    public ActionBarBehavior actionBarBehavior;
    public ShootingBehavior shootingBehavior;
    public BoomerangShooter boomerangShooter;
    public RayShooter rayShooter;
    public AddTilemap tilemapAdder;


    private GameObject _currentItem;
    private GameObject _prevItem;

    private void Start()
    {
        gameTimer.battleSystem.SetBattleState(BattleState.HUMANBUILD);
        foreach (var go in GameManager.globalManager.humanInventory.CurrentInventory)
        {
            go.transform.parent = mainCanvas.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
        }

        actionBarBehavior.SetList(GameManager.globalManager.humanInventory.CurrentInventory);
    }

    private void Update()
    {
        if (gameTimer.GetCurrentPlayer().Equals("Human"))
            UpdateHumanTurn();
        else
            UpdateAlienTurn();

        if (gameTimer.IsTimeUp() && gameTimer.GetCurrentPlayer().Equals("Alien"))
        {
            foreach (var go in GameManager.globalManager.alienInventory.CurrentInventory)
            {
                go.transform.parent = mainCanvas.transform;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                    go.GetComponent<RectTransform>().anchoredPosition3D.x,
                    go.GetComponent<RectTransform>().anchoredPosition3D.y, 1);
            }
            _currentItem = null;
            actionBarBehavior.Reset();
            actionBarBehavior.SetList(GameManager.globalManager.alienInventory.CurrentInventory);
            tilemapAdder.enabled = false;
        }
    }

    private void UpdateHumanTurn()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, buildingZone.position,
            534f * Time.smoothDeltaTime);
        mainCamera.orthographicSize =
            Mathf.MoveTowards(mainCamera.orthographicSize, 50f, 40f * Time.smoothDeltaTime);
        if (_currentItem != null)
        {
            Tile currentTile = new Tile();
            currentTile.sprite = _currentItem.GetComponent<Image>().sprite;
            tilemapAdder.UpdateTileArt(currentTile);
        }
    }

    private void UpdateAlienTurn()
    {
        #region moveAndScaleCamera
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, alienZone.position,
            86f * Time.smoothDeltaTime);
        mainCamera.orthographicSize =
            Mathf.MoveTowards(mainCamera.orthographicSize, 108f, 40f * Time.smoothDeltaTime);
        #endregion

        if (mainCamera.transform.position == alienZone.position)
            alienship.Init();

        if (_currentItem != null && _currentItem != _prevItem)
        {
            var weaponSO = weaponStore.GetWeaponFromBuyingSystemName(_currentItem.name);

            switch (weaponSO.travelBehavior)
            {
                case TravelBehavior.Magnet:
                    throw new NotImplementedException();

                case TravelBehavior.Boomerang:
                {
                    boomerangShooter.boomerangPrefab = weaponSO.prefab;
                    _prevItem = _currentItem;
                    break;
                }
                case TravelBehavior.Linear:
                {
                    rayShooter.rayPrefab = weaponSO.prefab;
                    _prevItem = _currentItem;
                    break;
                }

                case TravelBehavior.Projectile:
                {
                    var newProjectile = new GameObject
                    {
                        name = "Projectile"
                    };

                    newProjectile.AddComponent<SpriteRenderer>().sprite = _currentItem.GetComponent<Image>().sprite;
                    newProjectile.AddComponent<Rigidbody2D>();
                    newProjectile.AddComponent<BoxCollider2D>();
                    newProjectile.tag = "Weapon";
                    newProjectile.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    // the game object is needed to be used as a prefab
                    newProjectile.transform.position = new Vector3(-5000, -5000, -5000);
                    shootingBehavior.projectilePrefab = newProjectile;
                    _prevItem = _currentItem;
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }

    public void UseItem()
    {
        if (_currentItem.GetComponent<Item>().DecreaseCountInsureNonNegative() == 0 &&
            gameTimer.GetCurrentPlayer().Equals("Human"))
        {
            tilemapAdder.CreateTileMap();
            tilemapAdder.UpdateTileArt(null);
            _currentItem = null;
        }
    }

    public bool IsItemEmpty()
    {
        return _currentItem && _currentItem.GetComponent<Item>().Count == 0;
    }

    public void SetProjectile(GameObject newProjectile)
    {
        _prevItem = _currentItem;
        _currentItem = newProjectile;
    }
}