using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MECnt : MonoBehaviour
{
    public static int meCnt = 0;
    Text mExtractor;
    void Start()
    {
        mExtractor = GetComponent<Text> ();
    }

    void Update()
    {
        mExtractor.text =  meCnt.ToString();
    }
}
