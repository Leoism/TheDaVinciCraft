using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MetalCnt : MonoBehaviour
{
    public static int metalCnt = 0;
    Text metal;
    void Start()
    {
        metal = GetComponent<Text> ();
    }

    void Update()
    {
        metal.text =  metalCnt.ToString();
    }
}
