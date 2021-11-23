using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FinalRoundResults : MonoBehaviour
{
  public Text resultsText = null;
  // Start is called before the first frame update
  void Start()
  {
    List<List<Player>> rounds = GameManager.globalManager.Rounds;
    string resultsStr = "Final Round Results\n\n";
    for (int i = 0; i < rounds.Count; i++)
    {
      Player roundWinner = RoundWinner(rounds[i]);
      resultsStr += "Round " + (i + 1) + ": " + roundWinner.Name + " " + roundWinner.Type + "\n";
    }
    Player winner = GameManager.globalManager.GetWinner();
    resultsStr += "Winner: " + winner.Name + " " + winner.Type;
    resultsText.text = resultsStr;
  }

  private Player RoundWinner(List<Player> players)
  {
    Player currentWinner = null;
    for (int i = 0; i < players.Count; i++)
    {
      if (currentWinner == null)
      {
        currentWinner = players[i];
        continue;
      }
      if (players[i].Points > currentWinner.Points)
      {
        currentWinner = players[i];
      }
    }
    return currentWinner;
  }
}
