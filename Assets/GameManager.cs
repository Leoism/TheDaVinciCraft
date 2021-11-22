using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum GameMode
{
  SHORT = 3,
  STANDARD = 5,
  LONG = 7,
};

public class GameManager
{
  public static readonly GameManager globalManager = new GameManager();
  public GameMode gameMode;
  public Inventory humanInventory;
  public Inventory alienInventory;
  public Inventory artifacts;
  public BattleSystem battleSystem;
  public Player alienPlayer;
  public Player humanPlayer;

  public List<List<Player>> Rounds { get; private set; }

  private GameManager()
  {
  }

  public void Reset()
  {
    gameMode = GameMode.SHORT;
    humanInventory = null;
    alienInventory = null;
    artifacts = null;
    alienPlayer = null;
    humanPlayer = null;
    Rounds = null;
  }

  // Sets the weapons that the alien selected
  public void SetAlienInventory(List<GameObject> newAlienInventory)
  {
    alienInventory = new Inventory(newAlienInventory);
  }

  // Sets the materials that the human selected
  public void SetHumanInventory(List<GameObject> newHumanInventory)
  {
    humanInventory = new Inventory(newHumanInventory);
  }

  // Sets the artifacts to defend
  public void SetArtifacts(List<GameObject> newArtifacts)
  {
    artifacts = new Inventory(newArtifacts);
  }

  // Sets the game type for the game (Short, Standard, Long)
  public void SetGameType(GameMode newGameMode)
  {
    gameMode = newGameMode;
    Rounds = new List<List<Player>>();
  }

  /// Returns true if there are still rounds to complete, otherwise returns
  /// false
  public bool SaveRound(int humanScore, int alienScore)
  {
    Player alien = alienPlayer.Clone();
    alien.points = alienScore;
    Player human = humanPlayer.Clone();
    human.points = humanScore;
    Rounds.Add(new List<Player>() { human, alien });
    return Rounds.Count >= (int)gameMode;
  }

  // Sets the human player and alien player
  public void SetPlayers(Player human, Player alien)
  {
    humanPlayer = human;
    alienPlayer = alien;
  }

  // Returns whether a game ended with human or alien as winner, or tie
  public Player GetWinner()
  {
    int humanTotalScore = 0;
    int alienTotalScore = 0;
    foreach (List<Player> round in Rounds)
    {
      humanTotalScore += round[0].points;
      alienTotalScore += round[1].points;
    }

    return humanTotalScore > alienTotalScore ? humanPlayer : (alienTotalScore > humanTotalScore ? alienPlayer : null);
  }
}
