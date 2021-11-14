using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item 
{
    public enum MaterialType
    {
        Wood,
        Glass,
        Metal,
        Fabric,
        Stone
    }

    public enum WeaponType
    {
        Deforestor,
        Bomb,
        Boomerang,
        Magnet
    }

    public MaterialType materialType;
    public WeaponType weaponType;
    public int amount;
    
}
