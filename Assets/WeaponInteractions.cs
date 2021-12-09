using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponInteractions : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("floor"))
            StartCoroutine(waiter(this.gameObject));
    }
    IEnumerator waiter(GameObject collision)
    {
        //Wait for 3 seconds
        yield return new WaitForSeconds(3);
        Destroy(collision);
    }
}
