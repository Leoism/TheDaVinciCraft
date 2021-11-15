using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarBehavior : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> itemsAvailable = new List<GameObject>();

  void Start()
  {
    Render();
  }

  public void Render()
  {
    Vector3 nextPos = GetComponent<RectTransform>().anchoredPosition;
    for (int i = 0; i < itemsAvailable.Count; i++)
    {
      GameObject item = itemsAvailable[i];
      RectTransform itemRt = item.GetComponent<RectTransform>();
      Button itemButton = item.GetComponent<Button>();

      itemRt.anchoredPosition = nextPos;
      nextPos = itemRt.anchoredPosition - new Vector2(0, itemRt.rect.height);

      int tempIdx = i;
      // anonymous function to update count
      itemButton.onClick.AddListener(() =>
      {
        item.GetComponentInChildren<Text>().text = item.GetComponent<Item>().GetCount().ToString();
        UseItem(tempIdx);
      });
      item.transform.SetParent(transform);
    }
  }

  public void AddItem(GameObject newItem)
  {
    itemsAvailable.Add(newItem);
    Render();
  }

  public void SetList(List<GameObject> newList)
  {
    itemsAvailable = newList;
    Render();
  }

  public int UseItem(int itemIdx)
  {
    return itemsAvailable[itemIdx].GetComponent<Item>().DecreaseCount();
  }
}
