using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RayCnt : MonoBehaviour
{
    public static int rayCnt = 0;
    Text ray;
    void Start()
    {
        ray = GetComponent<Text> ();
    }

    void Update()
    {
        ray.text =  rayCnt.ToString();
    }
}
