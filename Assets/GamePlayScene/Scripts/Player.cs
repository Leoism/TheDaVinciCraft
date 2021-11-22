using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
  public int points = 0;
  public string name = "Default";
  public string type = "Default";
  public Player Clone()
  {
    return new Player
    {
      points = points,
      name = name,
      type = type
    };
  }
}
