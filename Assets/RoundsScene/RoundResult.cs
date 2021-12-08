using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundResult : MonoBehaviour
{
  public int humanScore = 0;
  public int alienScore = 0;
  void Awake()
  {
    GameManager.globalManager.SaveRound(humanScore, alienScore);
    GameManager.globalManager.allPlayersReady = false;
  }
}
