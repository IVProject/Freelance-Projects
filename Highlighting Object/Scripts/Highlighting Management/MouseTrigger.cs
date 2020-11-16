using UnityEngine;

namespace Proekt.HighlightingManagement {
    #region Attributes
    [AddComponentMenu("Highlighting Management/Mouse Trigger")]
    [DisallowMultipleComponent]
    [RequireComponent(typeof(Collider))]
    #endregion
    public class MouseTrigger : MonoBehaviour
    {
        [SerializeField] private Highlighting m_Highlighting;

        public Highlighting highlighting { get => m_Highlighting; set => m_Highlighting = value; }

        private void Reset()
        {
            if (TryGetComponent<HighlightingEmission>(out HighlightingEmission highlighting))
                m_Highlighting = highlighting;
            else
                m_Highlighting = gameObject.AddComponent<HighlightingEmission>();
        }

        private void OnMouseEnter()
        {
            m_Highlighting.Highlight();
        }

        private void OnMouseExit()
        {
            m_Highlighting.Normal();
        }

    }
}