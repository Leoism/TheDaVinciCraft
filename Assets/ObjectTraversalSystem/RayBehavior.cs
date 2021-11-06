using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RayBehavior : MonoBehaviour
{
  private Vector3 targetPosition;
  private float speed = 120f;
  // Start is called before the first frame update
  void Start()
  {

  }

  // Update is called once per frame
  void Update()
  {
    transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.smoothDeltaTime);
    transform.up = (targetPosition - transform.position).normalized;
  }

  public void SetTarget(Vector3 target)
  {
    targetPosition = target;
  }
}
