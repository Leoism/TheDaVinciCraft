using System;
using UnityEngine;

namespace StackingSystem.Scripts
{
    public class ByeBye : MonoBehaviour
    {
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
