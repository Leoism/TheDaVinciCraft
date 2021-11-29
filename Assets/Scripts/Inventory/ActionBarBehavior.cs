using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ActionBarBehavior : MonoBehaviour
{
  [SerializeField]
  private List<GameObject> itemsAvailable = new List<GameObject>();
  public TextMeshProUGUI lastSelectedItem = null;
  public Gameplay gameplayScene = null;
  void Start()
  {
  }

  void Update()
  {
  }

  public void Render()
  {
    Vector3 nextPos = GetComponent<RectTransform>().anchoredPosition;
    for (int i = 0; i < itemsAvailable.Count; i++)
    {
      GameObject item = itemsAvailable[i];
            Debug.Log(item.name);
      item.transform.SetParent(transform);
      RectTransform itemRt = item.GetComponent<RectTransform>();
      Button itemButton = item.GetComponent<Button>();
      itemButton.tag = "Button";
      itemRt.localScale = new Vector3(1, 1, 1);
      itemRt.anchoredPosition = nextPos;
      nextPos = itemRt.anchoredPosition - new Vector2(0, itemRt.rect.height);
      int tempIdx = i;
      item.GetComponentInChildren<Text>().text = item.GetComponent<Item>().GetCount().ToString();
            Tooltip newTT = item.AddComponent<Tooltip>();
            newTT.setMessage(item.GetComponent<Item>().GetMessage());
      // anonymous function to update count
      itemButton.onClick.AddListener(() =>
      {
        if (itemsAvailable[tempIdx].GetComponent<Item>().GetCount() > 0) {
        gameplayScene.SetProjectile(item);
        }
        lastSelectedItem.text = item.GetComponent<Item>().GetMessage();
        // UseItem(tempIdx);
        // item.GetComponentInChildren<Text>().text = item.GetComponent<Item>().GetCount().ToString();
      });
    }
  }

  public void Reset()
  {
    foreach (GameObject item in itemsAvailable)
    {
      Destroy(item);
    }
    itemsAvailable = null;
    lastSelectedItem.text = string.Empty;
  }

  public void AddItem(GameObject newItem)
  {
    itemsAvailable.Add(newItem);

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

  public string SetToolTip(string item)
    {
        Debug.Log(item);
        return item;
    }
}
