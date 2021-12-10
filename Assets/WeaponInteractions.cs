using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractions : MonoBehaviour
{
    // Start is called before the first frame updat

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Round(gameObject.GetComponent<Rigidbody2D>().velocity.magnitude) == 0) {
            StartCoroutine(waiter(gameObject));
        }
    }

    IEnumerator waiter(GameObject collision)
    {
        //Wait for 1.5 seconds
        yield return new WaitForSeconds(1.5f);
        Destroy(collision);
    }
}
