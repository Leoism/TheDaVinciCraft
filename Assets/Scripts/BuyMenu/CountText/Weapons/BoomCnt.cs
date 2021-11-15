using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BoomCnt : MonoBehaviour
{
    public static int boomCnt = 0;
    Text boomerange;
    void Start()
    {
        boomerange = GetComponent<Text> ();
    }

    void Update()
    {
        boomerange.text =  boomCnt.ToString();
    }
}
