using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GlassCnt : MonoBehaviour
{
    public static int glassCnt = 0;
    Text glass;
    void Start()
    {
        glass = GetComponent<Text> ();
    }

    void Update()
    {
        glass.text =  glassCnt.ToString();
    }
}
