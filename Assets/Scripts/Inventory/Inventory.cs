using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory {

    [SerializeField]
    private List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
        AddItem(new Item { materialType = Item.MaterialType.Wood, amount = 5 });
        AddItem(new Item { materialType = Item.MaterialType.Stone, amount = 4 });
        AddItem(new Item { materialType = Item.MaterialType.Metal, amount = 3 });
        AddItem(new Item { materialType = Item.MaterialType.Fabric, amount = 2 });
        AddItem(new Item { materialType = Item.MaterialType.Glass, amount = 1 });
        Debug.Log(itemList.Count);
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public List<Item> getItemList()
    {
        return itemList;
    }
}
