using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GamePlayScene.ScriptableObjects.WeaponsScriptableObject
{
    [CreateAssetMenu]
    public class WeaponStore : ScriptableObject
    {
        public List<WeaponScriptableObject> allWeapons;

        /// <summary>
        /// Attempts to get the corresponding WeaponScriptableObject
        /// by name as defined in the Scriptable objects
        /// </summary>
        /// <param name="weaponName"> The weapon's name </param>
        /// <returns> The WeaponScriptableObject if found, null otherwise </returns>
        public WeaponScriptableObject GetWeaponFromSOName(string weaponName)
        {
            return allWeapons.FirstOrDefault(w => w.name == weaponName);
        }

        /// <summary>
        /// Attempts to get the corresponding WeaponScriptableObject
        /// by name as defined in the Buying System
        /// </summary>
        /// <param name="weaponName"> The weapon's name </param>
        /// <returns> The WeaponScriptableObject if found, null otherwise </returns>
        public WeaponScriptableObject GetWeaponFromBuyingSystemName(string weaponName)
        {
            return weaponName switch
            {
                "deforestor" => GetWeaponFromSOName("Deforester"),
                "ball" => GetWeaponFromSOName("Bowling Ball"),
                "boomerang" => GetWeaponFromSOName("Boomerang"),
                "magnet" => GetWeaponFromSOName("Magnet"),
                "bomb" => GetWeaponFromSOName("Bomb"),
                "ray" => GetWeaponFromSOName("Eraser Ray"),
                "grenade" => GetWeaponFromSOName("Grenade"),
                "arrow" => GetWeaponFromSOName("Arrow"),
                _ => null
            };
        }

        /// <summary>
        /// Attempts to get the corresponding WeaponScriptableObject
        /// by name as defined in Scriptable objects then the Buying System
        /// </summary>
        /// <param name="weaponName"> The weapon's name</param>
        /// <returns> The WeaponScriptableObject if found, null otherwise </returns>
        public WeaponScriptableObject GetWeaponFromName(string weaponName)
        {
            return GetWeaponFromSOName(weaponName) ?? GetWeaponFromBuyingSystemName(weaponName);
        }
    }
}