using UnityEngine;

namespace GamePlayScene.ScriptableObjects.MaterialsScriptableObject
{
    public enum TravelBehavior
    {
        Magnet, Boomerang, Linear, Projectile
    }

    /// <summary>
    /// Holds the information for a material
    /// </summary>
    [CreateAssetMenu(menuName = "Material")]
    public class MaterialScriptableObject : ScriptableObject
    {
        public new string name;
        public Sprite sprite;
    }
}