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
  public readonly Timer Timer;
  public GameMode gameMode;
  public Timer timer = null;
  public Inventory humanInventory;
  public Inventory alienInventory;
  public Inventory artifacts;
  public BattleSystem battleSystem;
  public Player alienPlayer;
  public Player humanPlayer;

  private int _currentRound;

  public List<List<Player>> Rounds { get; private set; }

  private GameManager()
  {
    Timer = new Timer();
    _currentRound = 0;
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
    _currentRound = 0;
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
    Rounds.Add(new List<Player>
    {
      humanPlayer.Clone( newPoints: humanScore ),
      alienPlayer.Clone( newPoints: alienScore )
    });
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
      humanTotalScore += round[0].Points;
      alienTotalScore += round[1].Points;
    }

    return humanTotalScore > alienTotalScore ? humanPlayer : (alienTotalScore > humanTotalScore ? alienPlayer : null);
  }

  public string GetGameModeName()
  {
    return GameManager.globalManager.gameMode == GameMode.SHORT ? "Short" : (GameManager.globalManager.gameMode == GameMode.STANDARD ? "Standard" : "Long");
  }

  public List<int> GetMaterialCountForRound()
    {
        List<int> materialBuyingCount = new List<int>();
        int materialCount = 20;
        for (int i = 0; i < 13; i++)
        {
            materialBuyingCount.Add(materialCount);
            materialCount += 10;
        }
        return materialBuyingCount;
    }
    public List<int> GetWeaponCountForRound()
    {
        List<int> weaponBuyingCount = new List<int>();
        int weaponCount = 6;
        for (int i = 0; i < 13; i++)
        {
            weaponBuyingCount.Add(weaponCount);
            weaponCount += 3;
        }
        return weaponBuyingCount;
    }

    public int GetCurrentRound()
    {
        return _currentRound;
    }

    public void IncrementCurrRound()
    {
        _currentRound++;
    }

    public void ResetRound()
    {
        _currentRound = 0;
    }
}
