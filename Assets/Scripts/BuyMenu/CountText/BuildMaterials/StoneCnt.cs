using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StoneCnt : MonoBehaviour
{
    public static int stoneCnt = 0;
    Text stone;
    void Start()
    {
        stone = GetComponent<Text> ();
    }

    // Update is called once per frame
    void Update()
    {
        stone.text =  stoneCnt.ToString();
    }
}
