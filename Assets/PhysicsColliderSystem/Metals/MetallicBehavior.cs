using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MetallicBehavior : MonoBehaviour
{
  void OnTriggerStay2D(Collider2D collision)
  {
    GameObject go = collision.gameObject;
    if (go.name.Contains("Magnet Top"))
    {
      MagnetBehavior mb = go.GetComponentInParent<MagnetBehavior>();
      transform.rotation = mb.GetMagnetRotation();
    }
    else if (go.name.Contains("Magnet"))
    {
      MagnetBehavior mb = go.GetComponent<MagnetBehavior>();
      float distance = Mathf.Abs(Vector3.Distance(transform.position, mb.tip.position));
      float strengthMultiplier = 1 - (distance / 30f);
      transform.position = Vector3.MoveTowards(transform.position, mb.tip.position, mb.GetMagnetStrength() * strengthMultiplier);
      transform.up = (mb.tip.position - transform.position).normalized;
    }
  }
}
