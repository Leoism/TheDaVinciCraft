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
  public static GameManager globalManager = new GameManager();
  public GameMode gameMode;
  public Timer timer = null;
  public Inventory humanInventory;
  public Inventory alienInventory;
  public Inventory artifacts;
  public BattleSystem battleSystem;
  public Player alienPlayer = null;
  public Player humanPlayer = null;
  private List<List<Player>> rounds = null;

  private GameManager()
  {
    timer = new Timer();
  }

  // Sets the weapons that the alien selected
  public void SetAlienInventory(List<GameObject> newAlienInventory)
  {
    GameManager.globalManager.alienInventory = new Inventory(newAlienInventory);
  }

  // Sets the materials that the human selected
  public void SetHumanInventory(List<GameObject> newHumanInventory)
  {
    GameManager.globalManager.humanInventory = new Inventory(newHumanInventory);
  }

  // Sets the artifacts to defend
  public void SetArtifacts(List<GameObject> newArtifacts)
  {
    GameManager.globalManager.artifacts = new Inventory(newArtifacts);
  }

  // Sets the game type for the game (Short, Standard, Long)
  public void SetGameType(GameMode newGameMode)
  {
    GameManager.globalManager.gameMode = newGameMode;
    GameManager.globalManager.rounds = new List<List<Player>>();
  }

  /// Returns true if there are still rounds to complete, otherwise returns
  /// false
  public bool SaveRound(int humanScore, int alienScore)
  {
    Player alien = alienPlayer.Clone();
    alien.points = alienScore;
    Player human = humanPlayer.Clone();
    human.points = humanScore;
    GameManager.globalManager.rounds.Add(new List<Player>() { human, alien });
    return GameManager.globalManager.rounds.Count >= (int)GameManager.globalManager.gameMode;
  }

  // Sets the human player and alien player
  public void SetPlayers(Player human, Player alien)
  {
    GameManager.globalManager.humanPlayer = human;
    GameManager.globalManager.alienPlayer = alien;
  }

  // Returns the rounds
  public List<List<Player>> GetRounds()
  {
    return GameManager.globalManager.rounds;
  }

  // Returns whether a game ended with human or alien as winner, or tie
  public string GetWinner()
  {
    int humanTotalScore = 0;
    int alienTotalScore = 0;
    foreach (List<Player> round in rounds)
    {
      humanTotalScore += round[0].points;
      alienTotalScore += round[1].points;
    }

    return humanTotalScore > alienTotalScore ? "human" : (alienTotalScore > humanTotalScore ? "alien" : "tie");
  }

  public string GetGameModeName()
  {
    return GameManager.globalManager.gameMode == GameMode.SHORT ? "Short" : (GameManager.globalManager.gameMode == GameMode.STANDARD ? "Standard" : "Long");
  }
}
