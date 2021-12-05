using System;
using UnityEngine;
using UnityEngine.Tilemaps;
namespace StackingSystem.Scripts
{
    public class ByeBye : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
        void OnMouseOver()
        {
            // this object was clicked - do something
            if (Input.GetMouseButtonDown(1)) {
                int usedTiles = 0;
                TileBase[] allTiles = gameObject.GetComponent<Tilemap>().GetTilesBlock(gameObject.GetComponent<Tilemap>().cellBounds);
                for (int x = 0; x < gameObject.GetComponent<Tilemap>().cellBounds.size.x; x++)
                {
                    for (int y = 0; y < gameObject.GetComponent<Tilemap>().cellBounds.size.y; y++)
                    {
                        TileBase tile = allTiles[x + y * gameObject.GetComponent<Tilemap>().cellBounds.size.x];
                        if (tile != null)
                        {
                            usedTiles++;
                        }
                    }
                }
                string erasedName = gameObject.tag.Substring(0, gameObject.tag.IndexOf("Tile"));
                GameObject foundObject = GameManager.globalManager.humanInventory.GetInventoryList().Find((item) => item.GetComponent<Item>().GetItemName().Equals(erasedName));
                Item foundItem = foundObject.GetComponent<Item>();
                foundItem.SetCount(foundItem.GetCount() + usedTiles);
                Destroy(this.gameObject);
            }
        }
    }
}
