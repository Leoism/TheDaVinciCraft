using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
  private int count = 0;
  private string itemName;
  private string message;
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
  
    public void SetMessage(string msg)
    {
        message = msg;
    }

    public string GetMessage()
    {
        return message;
    }
    
    
    public void SetItemName(string name) {
      itemName = name;
    }

    public string GetItemName() {
      return itemName;
    }

}
