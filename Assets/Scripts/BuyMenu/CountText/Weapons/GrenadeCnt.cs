using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrenadeCnt : MonoBehaviour
{
    public static int grCnt = 0;
    Text grenade;
    void Start()
    {
        grenade = GetComponent<Text> ();
    }

    void Update()
    {
        grenade.text =  grCnt.ToString();
    }
}
