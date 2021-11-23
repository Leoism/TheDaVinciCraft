using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentRoom : MonoBehaviour
{
  private RoomsCanvases _roomCanvases;
  // Start is called before the first frame update
  public void Initialize(RoomsCanvases canvases)
  {
    _roomCanvases = canvases;
  }

  public void Show()
  {
    gameObject.SetActive(true);
    _roomCanvases.CreateOrJoin.Hide();
  }

  public void Hide()
  {
    gameObject.SetActive(false);
  }
}
