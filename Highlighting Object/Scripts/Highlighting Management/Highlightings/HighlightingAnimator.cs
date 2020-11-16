using UnityEngine;

namespace Proekt.HighlightingManagement
{
    #region Attributes
    [HighlightingItem("Highlighting Animator")]
    [AddComponentMenu("Highlighting Management/Highlightings/Highlighting Animator")]
    [RequireComponent(typeof(Animator))]
    #endregion
    public class HighlightingAnimator : Highlighting
    {
        [Header("Triggers Name")]
        [SerializeField] private string m_Normal = "Normal";
        [SerializeField] private string m_Highlighted = "Highlighted";

        private Animator m_Animator;

        public Animator animator { get => m_Animator != null ? m_Animator : m_Animator = GetComponent<Animator>(); }

        public string normal { get => m_Normal; }

        public string highlighted { get => m_Highlighted; }

        public override void Highlight()
        {
            animator.SetTrigger(m_Highlighted);
        }

        public override void Normal()
        {
            animator.SetTrigger(m_Normal);
        }
    }
}
