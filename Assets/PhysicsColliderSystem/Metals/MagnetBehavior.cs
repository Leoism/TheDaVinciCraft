using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetBehavior : MonoBehaviour
{
  private float magnetStrength = 3f;
  public Transform tip = null;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
  }

  public void SetMagnetStrength(float strength)
  {
    magnetStrength = strength;
  }

  public float GetMagnetStrength()
  {
    return magnetStrength;
  }

  public Quaternion GetMagnetRotation()
  {
    return transform.rotation;
  }
}
