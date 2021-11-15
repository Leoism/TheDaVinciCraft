using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class ArtDB : ScriptableObject
{
    public Arts[] arts;
    public int ArtsCount
    {
        get
        {
            return arts.Length;
        }
    }
    public Arts GetArts(int index)
    {
        return arts[index];
    }
}
