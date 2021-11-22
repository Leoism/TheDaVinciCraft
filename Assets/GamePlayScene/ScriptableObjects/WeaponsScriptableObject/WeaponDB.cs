using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlayScene.ScriptableObjects.WeaponsScriptableObject
{
    [CreateAssetMenu]
    public class WeaponDB : ScriptableObject
    {
        public List<WeaponScriptableObject> allWeapons;

        public WeaponScriptableObject GetWeaponSOFromName(string weaponName)
        {
            return allWeapons.First(w => w.name == weaponName);
        }
    }
}