using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FebricCnt : MonoBehaviour
{
    public static int febricCnt = 0;
    Text febric;
    void Start()
    {
        febric = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        febric.text =  febricCnt.ToString();
    }
}
