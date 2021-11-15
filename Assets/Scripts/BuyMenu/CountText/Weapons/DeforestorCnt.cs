using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeforestorCnt : MonoBehaviour
{
    public static int deCnt = 0;
    Text deforestor;
    void Start()
    {
        deforestor = GetComponent<Text> ();
    }

    void Update()
    {
        deforestor.text =  deCnt.ToString();
    }
}
