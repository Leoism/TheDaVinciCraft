using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gameplay : MonoBehaviour
{
  public Timer gameTimer = null;
  public Camera mainCamera = null;
  public Transform buildingZone = null;
  public Transform alienZone = null;
  public Alienship alienship = null;
  public Canvas mainCanvas = null;
  public ActionBarBehavior actionBarBehavior = null;
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
      mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, buildingZone.position, 85f * Time.smoothDeltaTime);
      mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, 50f, 40f * Time.smoothDeltaTime);
    }
    else
    {
      mainCamera.transform.position = Vector3.MoveTowards(mainCamera.transform.position, alienZone.position, 85f * Time.smoothDeltaTime);
      mainCamera.orthographicSize = Mathf.MoveTowards(mainCamera.orthographicSize, 108f, 40f * Time.smoothDeltaTime);
      if (mainCamera.transform.position == alienZone.position)
        alienship.Init();
    }
  }
}
