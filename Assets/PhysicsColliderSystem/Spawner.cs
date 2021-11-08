using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
  public GameObject thingToSpawn = null;
  // Update is called once per frame
  void Update()
  {
    Vector3 mousePos = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
    if (Input.GetMouseButtonDown(0))
    {
      Instantiate(thingToSpawn, mousePos, Quaternion.identity);
    }
  }
}
