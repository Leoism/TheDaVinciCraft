using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  private int count = 0;

  public int IncreaseCount()
  {
    return ++count;
  }

  public int DecreaseCount()
  {
    if (count - 1 >= 0)
      count--;
    return count;
  }

  public void SetCount(int newCount)
  {
    count = newCount;
  }

  public int GetCount()
  {
    return count;
  }
}
