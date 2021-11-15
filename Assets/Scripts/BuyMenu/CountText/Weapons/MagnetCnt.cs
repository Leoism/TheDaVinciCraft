using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MagnetCnt : MonoBehaviour
{
    public static int magCnt = 0;
    Text magnet;
    void Start()
    {
        magnet = GetComponent<Text> ();
    }

    void Update()
    {
        magnet.text =  magCnt.ToString();
    }
}
