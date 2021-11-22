using System;
using GamePlayScene.ScriptableObjects.MaterialsScriptableObject;
using GamePlayScene.ScriptableObjects.WeaponsScriptableObject;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    public WeaponDB weaponDB;
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
    public GameObject currentItem;
    public GameObject prevItem;

    private void Start()
    {
        gameTimer.battleSystem.SetBattleState(BattleState.HUMANBUILD);
        foreach (var go in GameManager.globalManager.humanInventory.GetInventoryList())
        {
            go.transform.parent = mainCanvas.transform;
            go.transform.localScale = new Vector3(1, 1, 1);
        }

        GameManager.globalManager.humanInventory.actionBar = actionBarBehavior;
        actionBarBehavior.SetList(GameManager.globalManager.humanInventory.GetInventoryList());
    }

    private void Update()
    {
        if (gameTimer.GetCurrentPlayer().Equals("Human"))
            UpdateHumanTurn();
        else
            UpdateAlienTurn();

        if (gameTimer.IsTimeUp() && gameTimer.GetCurrentPlayer().Equals("Alien"))
        {
            foreach (var go in GameManager.globalManager.alienInventory.GetInventoryList())
            {
                go.transform.parent = mainCanvas.transform;
                go.transform.localScale = new Vector3(1, 1, 1);
                go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(
                    go.GetComponent<RectTransform>().anchoredPosition3D.x,
                    go.GetComponent<RectTransform>().anchoredPosition3D.y, 1);
            }

            currentItem = null;
            actionBarBehavior.Reset();
            GameManager.globalManager.alienInventory.actionBar = actionBarBehavior;
            actionBarBehavior.SetList(GameManager.globalManager.alienInventory.GetInventoryList());
            tilemapAdder.enabled = false;
        }
    }

    private void UpdateHumanTurn()
    {
        mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, buildingZone.position,
            534f * Time.smoothDeltaTime);
        mainCamera.orthographicSize =
            Mathf.MoveTowards(mainCamera.orthographicSize, 50f, 40f * Time.smoothDeltaTime);
        if (currentItem != null)
        {
            Tile currentTile = new Tile();
            currentTile.sprite = currentItem.GetComponent<Image>().sprite;
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

        if (currentItem != null && currentItem != prevItem)
        {
            var weaponSO = weaponDB.GetWeaponFromBuyingSystemName(currentItem.name);

            switch (weaponSO.travelBehavior)
            {
                case TravelBehavior.Magnet:
                    throw new NotImplementedException();

                case TravelBehavior.Boomerang:
                case TravelBehavior.Linear:
                {
                    rayShooter.rayPrefab = weaponSO.prefab;
                    break;
                }

                case TravelBehavior.Projectile:
                {
                    var newProjectile = new GameObject
                    {
                        name = "Projectile"
                    };

                    newProjectile.AddComponent<SpriteRenderer>().sprite = currentItem.GetComponent<Image>().sprite;
                    newProjectile.AddComponent<Rigidbody2D>();
                    newProjectile.AddComponent<BoxCollider2D>();
                    newProjectile.tag = "Weapon";
                    newProjectile.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
                    // the game object is needed to be used as a prefab
                    newProjectile.transform.position = new Vector3(-5000, -5000, -5000);
                    shootingBehavior.projectilePrefab = newProjectile;
                    prevItem = currentItem;
                    break;
                }

                default:
                    throw new ArgumentOutOfRangeException();
            }

        }
    }

    public void UseItem()
    {
        if (currentItem.GetComponent<Item>().DecreaseCount() == 0 &&
            gameTimer.GetCurrentPlayer().Equals("Human"))
        {
            tilemapAdder.CreateTileMap();
            tilemapAdder.UpdateTileArt(null);
            currentItem = null;
        }
    }

    public bool IsItemEmpty()
    {
        return currentItem && currentItem.GetComponent<Item>().GetCount() == 0;
    }

    public void SetProjectile(GameObject newProjectile)
    {
        prevItem = currentItem;
        currentItem = newProjectile;
    }
}