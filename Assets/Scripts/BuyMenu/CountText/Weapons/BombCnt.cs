using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BombCnt : MonoBehaviour
{
    public static int bombCnt = 0;
    Text bomb;
    void Start()
    {
        bomb = GetComponent<Text> ();
    }

    void Update()
    {
        bomb.text =  bombCnt.ToString();
    }
}
