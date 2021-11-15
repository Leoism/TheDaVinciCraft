using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class turnManager : MonoBehaviour
{
    public GameObject shoot;
    public GameObject place;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey("z")) {
           shoot.GetComponent<ShootingBehavior>().enabled = false;
            place.GetComponent<AddTilemap>().enabled = true;
           // place.SetActive(true);
        }
        else if (Input.GetKey("x"))
        {
            place.GetComponent<AddTilemap>().enabled = false;
            shoot.GetComponent<ShootingBehavior>().enabled = true;
            //shoot.GetComponent<ShootingBehavior>().enabled = true;
        }
        
    }
}
