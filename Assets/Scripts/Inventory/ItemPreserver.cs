using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPreserver : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
