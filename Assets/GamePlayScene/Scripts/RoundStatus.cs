using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RoundStatus : MonoBehaviour
{
  public Text roundText = null;
  // Start is called before the first frame update
  void Awake()
  {
    roundText.text = "ROUND: " + (GameManager.globalManager.GetRounds().Count + 1).ToString();
  }
}
