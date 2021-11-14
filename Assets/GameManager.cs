using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager
{
  public static GameManager globalManager = new GameManager();
  public Timer timer = null;
  public Inventory humanInventory;
  public Inventory alienInventory;
  public Inventory artifacts;
  public BattleSystem battleSystem;

  private GameManager()
  {
    timer = new Timer();
  }

  public void SetAlienInventory(List<GameObject> newAlienInventory)
  {
    GameManager.globalManager.alienInventory = new Inventory(newAlienInventory);
  }

  public void SetHumanInventory(List<GameObject> newHumanInventory)
  {
    GameManager.globalManager.humanInventory = new Inventory(newHumanInventory);
  }

  public void SetArtifacts(List<GameObject> newArtifacts)
  {
    GameManager.globalManager.artifacts = new Inventory(newArtifacts);
  }
}
