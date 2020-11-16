using UnityEngine;

namespace Proekt.HighlightingManagement
{
    [DisallowMultipleComponent]
    public abstract class Highlighting : MonoBehaviour
    {
        public abstract void Normal();
        public abstract void Highlight();
    }
}
