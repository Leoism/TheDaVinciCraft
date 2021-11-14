using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alienship : MonoBehaviour
{
  // Start is called before the first frame update
  void Start()
  {
    Vector3 windowCorner = Camera.main.ViewportToWorldPoint(new Vector3(0.8f, 0.85f, 1));
    transform.position = windowCorner;
  }
}
