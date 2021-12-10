using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractions : MonoBehaviour
{
  bool isCoroutineRunning = false;
  // Start is called before the first frame updat

  // Update is called once per frame
  void Update()
  {
    float speed = Mathf.Round(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude);
    if (speed == 0)
    {
      StartCoroutine(waiter(gameObject));
    }
    else if (speed > 0 && isCoroutineRunning)
    {
      StopCoroutine(waiter(gameObject));
      isCoroutineRunning = false;
    }
  }

  IEnumerator waiter(GameObject collision)
  {
    isCoroutineRunning = true;
    //Wait for 1.5 seconds
    yield return new WaitForSeconds(1.5f);
    if (isCoroutineRunning)
      Destroy(collision);
  }
}
