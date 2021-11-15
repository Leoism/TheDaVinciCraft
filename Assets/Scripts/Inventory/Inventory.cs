using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
  public ActionBarBehavior actionBar = null;
  [SerializeField]
  private List<GameObject> currentInventory;
  private GameObject currentInUse = null;
  public Inventory(List<GameObject> inventory)
  {
    SetInventory(inventory);
  }

  public void SetInventory(List<GameObject> newInventory)
  {
    currentInventory = newInventory;
  }

  // Creates an actionbar
  public void InitActionBar()
  {
    if (actionBar)
      actionBar.SetList(currentInventory);
  }

  // "Uses up" on item 
  public void UseItem(int itemIdx)
  {
    currentInUse = currentInventory[itemIdx];
    currentInUse.GetComponent<Item>().DecreaseCount();
  }

  // retrieves the total amount of inventory count
  public int TotalCount()
  {
    int sum = 0;
    foreach (GameObject item in currentInventory)
    {
      sum += item.GetComponent<Item>().GetCount();
    }
    return sum;
  }
}
