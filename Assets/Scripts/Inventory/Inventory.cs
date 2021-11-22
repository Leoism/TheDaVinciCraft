using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory
{
  public List<GameObject> CurrentInventory { get; }

  public Inventory(List<GameObject> inventory)
  {
    CurrentInventory = inventory;
  }

  // retrieves the total amount of inventory count
  public int TotalCount()
  {
    return CurrentInventory.Sum(o => o.GetComponent<Item>().GetCount());
  }
}
