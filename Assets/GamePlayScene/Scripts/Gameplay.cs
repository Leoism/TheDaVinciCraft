using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Tilemaps;
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
  public GameObject prevItem = null;
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
        Tile currentTile = new Tile();
        currentTile.sprite = currentItem.GetComponent<Image>().sprite;
        tilemapAdder.UpdateTileArt(currentTile);
      }
    }
    else
    {

      mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, alienZone.position, 86f * Time.smoothDeltaTime);
      mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, 108f, 40f * Time.smoothDeltaTime);
      if (mainCamera.transform.position == alienZone.position)
        alienship.Init();
      if (currentItem != null && currentItem != prevItem)
      {
        GameObject newProjectile = Instantiate(currentItem);
        newProjectile.AddComponent<SpriteRenderer>().sprite = currentItem.GetComponent<Image>().sprite;
        newProjectile.AddComponent<Rigidbody2D>();
        newProjectile.AddComponent<BoxCollider2D>();
        newProjectile.GetComponent<ItemPreserver>().enabled = false;
        newProjectile.tag = "Weapon";
        newProjectile.transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        shootingBehavior.projectilePrefab = newProjectile;
        prevItem = currentItem;
      }
    }

    if (gameTimer.IsTimeUp() && gameTimer.GetCurrentPlayer().Equals("Alien"))
    {
      foreach (GameObject go in GameManager.globalManager.alienInventory.GetInventoryList())
      {
        go.transform.parent = mainCanvas.transform;
        go.transform.localScale = new Vector3(1, 1, 1);
        go.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(go.GetComponent<RectTransform>().anchoredPosition3D.x, go.GetComponent<RectTransform>().anchoredPosition3D.y, 1);
      }
      currentItem = null;
      actionBarBehavior.Reset();
      GameManager.globalManager.alienInventory.actionBar = actionBarBehavior;
      actionBarBehavior.SetList(GameManager.globalManager.alienInventory.GetInventoryList());
      tilemapAdder.enabled = false;
    }
  }

  public void UseItem()
  {
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
    return currentItem.GetComponent<Item>().GetCount() == 0;
  }

  public void SetProjectile(GameObject newProjectile)
  {
    prevItem = currentItem;
    currentItem = newProjectile;
  }
}
