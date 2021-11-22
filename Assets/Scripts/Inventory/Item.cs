using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public int Count { get; set; }
    public string Message { get; set; }

    public int DecreaseCountInsureNonNegative()
    {
        if (Count - 1 >= 0)
            Count--;
        return Count;
    }
}