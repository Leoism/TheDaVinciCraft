using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;

public class Player
{
  public int points = 0;
  public string name = "Default";
  public string type = "Default";
  public Player Clone(int? newPoints = null, [CanBeNull] string newName = null, [CanBeNull] string newType = null)
  {
    return new Player
    {
      points = newPoints ?? points,
      name = newName ?? name,
      type = newType ?? type
    };
  }
}
