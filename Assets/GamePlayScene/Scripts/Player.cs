using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
  public int points = 0;
  public string name = "Default";

  public Player Clone()
  {
    Player newPlayer = new Player();
    newPlayer.points = points;
    newPlayer.name = name;
    return newPlayer;
  }
}
