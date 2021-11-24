using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ModeGroup : MonoBehaviour
{
  public ToggleGroup toggleGroup;
  void Start()
  {
    toggleGroup = GetComponent<ToggleGroup>();
  }
}
