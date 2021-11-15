using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ArrowCnt : MonoBehaviour
{
    public static int arrCnt = 0;
    Text arrow;
    void Start()
    {
        arrow = GetComponent<Text> ();
    }

    void Update()
    {
        arrow.text =  arrCnt.ToString();
    }
}
