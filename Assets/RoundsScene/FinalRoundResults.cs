using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
public class FinalRoundResults : MonoBehaviour
{
  public Text resultsText = null;
  // Start is called before the first frame update
  void Start()
  {
    List<List<Player>> rounds = GameManager.globalManager.GetRounds();
    string resultsStr = "Final Round Results\n\n";
    for (int i = 0; i < rounds.Count; i++)
    {
      Player roundWinner = RoundWinner(rounds[i]);
      resultsStr += "Round " + (i + 1) + ": " + roundWinner.name + " " + roundWinner.type + "\n";
    }
    Player winner = GameManager.globalManager.GetWinner();
    resultsStr += "Winner: " + winner.name + " " + winner.type;
    resultsText.text = resultsStr;
    if (GameManager.globalManager.isOnlineMode)
    {
      // After final scene, we want to disconnect the two players
      PhotonNetwork.Disconnect();
    }
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
      if (players[i].points > currentWinner.points)
      {
        currentWinner = players[i];
      }
    }
    return currentWinner;
  }
}
