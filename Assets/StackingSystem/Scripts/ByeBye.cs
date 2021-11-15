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
        void OnMouseOver()
        {
            // this object was clicked - do something
            if (Input.GetMouseButtonDown(1))
                Destroy(this.gameObject);
        }
    }
}
