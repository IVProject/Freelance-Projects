using UnityEditor;
using UnityEditor.UI;
using InvsoftEngine.UI;

namespace InvsoftEditor.UI
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(Swich), true)]
    public class SwichEditor : SelectableEditor
    {
        SerializedProperty m_FillRect;
        SerializedProperty m_HandleRect;
        SerializedProperty m_Value;
        SerializedProperty m_OnChangeValue;

        private void OnEnable()
        {
            base.OnEnable();
            m_FillRect = serializedObject.FindProperty("m_FillRect");
            m_HandleRect = serializedObject.FindProperty("m_HandleRect");
            m_Value = serializedObject.FindProperty("m_Value");
            m_OnChangeValue = serializedObject.FindProperty("m_OnChangeValue");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            EditorGUILayout.PropertyField(m_FillRect);
            EditorGUILayout.PropertyField(m_HandleRect);
            EditorGUILayout.PropertyField(m_Value);
            EditorGUILayout.PropertyField(m_OnChangeValue);

            serializedObject.ApplyModifiedProperties();
        }
    }
}
