using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WoodCnt : MonoBehaviour
{
    // Start is called before the first frame update
    public static int woodCnt = 0;
    Text wood;
    void Start()
    {
        wood = GetComponent<Text> ();
    }

    void Update()
    {
        wood.text =  woodCnt.ToString();
    }
}