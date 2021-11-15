using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionBarItem : MonoBehaviour
{
  public Text currentCount = null;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {

  }

  public void SetCount(string newCount)
  {
    currentCount.text = newCount;
  }
}
