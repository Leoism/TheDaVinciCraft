using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrappleCnt : MonoBehaviour
{
    public static int grappleCnt = 0;
    Text grapple;
    void Start()
    {
        grapple = GetComponent<Text> ();
    }

    void Update()
    {
        grapple.text =  grappleCnt.ToString();
    }
}
