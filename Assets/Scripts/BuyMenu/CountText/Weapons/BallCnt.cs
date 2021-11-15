using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BallCnt : MonoBehaviour
{
    public static int ballCnt = 0;
    Text ball;
    void Start()
    {
        ball = GetComponent<Text> ();
    }

    void Update()
    {
        ball.text =  ballCnt.ToString();
    }
}
