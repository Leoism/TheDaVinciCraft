using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
using Photon.Pun;
public class Gameplay : MonoBehaviour
{
  public Timer gameTimer = null;
  public Camera mainCamera = null;
  public Transform buildingZone = null;
  public Transform alienZone = null;
  public Alienship alienship = null;
  public Canvas mainCanvas = null;
  public ActionBarBehavior actionBarBehavior = null;
  public ShootingBehavior shootingBehavior = null;
  public AddTilemap tilemapAdder = null;
  public GameObject currentItem = null;
  // Start is called before the first frame update
  void Start()
  {
    gameTimer.battleSystem.SetBattleState(BattleState.HUMANBUILD);
    foreach (GameObject go in GameManager.globalManager.humanInventory.GetInventoryList())
    {
      go.transform.parent = mainCanvas.transform;
      go.transform.localScale = new Vector3(1, 1, 1);
    }
    GameManager.globalManager.humanInventory.actionBar = actionBarBehavior;
    actionBarBehavior.SetList(GameManager.globalManager.humanInventory.GetInventoryList());
  }

  // Update is called once per frame
  void Update()
  {
    if (gameTimer.GetCurrentPlayer().Equals("Human"))
    {
      mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, buildingZone.position, 534f * Time.smoothDeltaTime);
      mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, 50f, 40f * Time.smoothDeltaTime);
      if (currentItem != null)
      {
        // Updates the tile image to use for tiles
        Tile currentTile = new Tile();
        currentTile.sprite = currentItem.GetComponent<Image>().sprite;
        currentTile.name = currentItem.name;
        tilemapAdder.UpdateTileArt(currentTile);
      }
    }
    else
    {
      mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, alienZone.position, 86f * Time.smoothDeltaTime);
      mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, 108f, 40f * Time.smoothDeltaTime);
      alienship.Init();
    }

    if (gameTimer.IsTimeUp() && gameTimer.GetCurrentPlayer().Equals("Alien"))
    {
      currentItem = null;
      foreach (GameObject go in GameManager.globalManager.alienInventory.GetInventoryList())
      {
        go.transform.parent = mainCanvas.transform;
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(go.GetComponent<RectTransform>().anchoredPosition3D.x, go.GetComponent<RectTransform>().anchoredPosition3D.y, 1);
      }

      actionBarBehavior.Reset();
      GameManager.globalManager.alienInventory.actionBar = actionBarBehavior;
      actionBarBehavior.SetList(GameManager.globalManager.alienInventory.GetInventoryList());
      tilemapAdder.enabled = false;
    }
  }

  public void UseItem()
  {
    if (currentItem == null) return;
    if (currentItem.GetComponent<Item>().DecreaseCount() == 0)
    {
      if (gameTimer.GetCurrentPlayer().Equals("Human"))
      {
        tilemapAdder.CreateTileMap();
        tilemapAdder.UpdateTileArt(null);
        currentItem = null;
      }
    }
  }

  public bool IsItemEmpty()
  {
    return currentItem && currentItem.GetComponent<Item>().GetCount() == 0;
  }

  public void SetProjectile(GameObject newProjectile)
  {
    currentItem = newProjectile;
    currentItem.name = newProjectile.name;
    if (currentItem != null)
    {
      GameObject newProj = (GameObject)Resources.Load("GamePlayScene/Projectile");
      newProj.transform.localScale = new Vector3(5f, 5f, 5f);
      newProj.GetComponent<SpriteRenderer>().sprite = currentItem.GetComponent<Image>().sprite;
      shootingBehavior.projectilePrefab = newProj;
      shootingBehavior.projectileName = currentItem.name;
    }
  }
}
