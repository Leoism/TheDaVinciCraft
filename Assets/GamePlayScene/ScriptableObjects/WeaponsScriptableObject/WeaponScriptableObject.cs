using System.Collections.Generic;
using GamePlayScene.ScriptableObjects.MaterialsScriptableObject;
using UnityEngine;

namespace GamePlayScene.ScriptableObjects.WeaponsScriptableObject
{
    /// <summary>
    /// Holds the information for a weapon
    /// </summary>
    [CreateAssetMenu(menuName = "Weapon")]
    public class WeaponScriptableObject : ScriptableObject
    {
        public new string name;
        public GameObject prefab;

        public List<MaterialScriptableObject> strongAgainst;
        public TravelBehavior travelBehavior;
    }
}
