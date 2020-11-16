using UnityEngine;
using UnityEditor;
using Proekt.HighlightingManagement;

namespace ProektEditor.HighlightingManagement
{
    [CustomEditor(typeof(HighlightingAnimator))]
    public class HighlightingAnimatorEditor : Editor
    {
        private HighlightingAnimator m_HighlightingAnimator;
        private const string m_MessageText = "The animator has no controller! Add a ready-made controller or create a new one automatically.";

        private void OnEnable()
        {
            m_HighlightingAnimator = (HighlightingAnimator)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if(m_HighlightingAnimator.animator.runtimeAnimatorController == null)
            {
                EditorGUILayout.HelpBox(m_MessageText, MessageType.Warning);
                if (GUILayout.Button("Auto Generate Animation"))
                {
                    HighlightingAnimatorController.TryCreateControllerFromPanel(m_HighlightingAnimator);
                }
            }
        }
    }
}
