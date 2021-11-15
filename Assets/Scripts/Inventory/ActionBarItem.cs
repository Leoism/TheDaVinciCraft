using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarItem : MonoBehaviour
{
  public Text currentCount = null;
  public Item actionItem = null;
  // Start is called before the first frame update
  void Start()
  {
    currentCount = GetComponentInChildren<Text>();
  }

  // Update is called once per frame
  void Update()
  {
    currentCount.text = actionItem.GetCount().ToString();
  }

  public void SetCount(string newCount)
  {
    currentCount.text = newCount;
  }
}
