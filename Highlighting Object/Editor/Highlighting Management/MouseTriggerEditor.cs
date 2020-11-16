using System;
using System.Reflection;
using UnityEngine;
using UnityEditor;
using Proekt.HighlightingManagement;

namespace ProektEditor.HighlightingManagement
{
    [CustomEditor(typeof(MouseTrigger))]
    public class MouseTriggerEditor : Editor
    {
        private MouseTrigger m_MouseTrigger;
        private GameObject m_GameObjectCache;
        private const string m_MessageText = "There is no essential component to work with! Add it to the empty field or use the button below.";

        private GameObject gameObjectCache { get => m_GameObjectCache != null ?
                m_GameObjectCache : m_GameObjectCache = m_MouseTrigger.gameObject; }

        private void OnEnable()
        {
            m_MouseTrigger = (MouseTrigger)target;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            if (m_MouseTrigger.highlighting == null)
            {
                EditorGUILayout.HelpBox(m_MessageText, MessageType.Warning);
            }

            if (GUILayout.Button("Replace highlighting component"))
            {
                TryReplaceFromContextMenu(m_MouseTrigger.highlighting);
            }
        }

        private void TryReplaceFromContextMenu(Highlighting highlighting)
        {
            GenericMenu menu = new GenericMenu();

            foreach (TypeInfo type in typeof(Highlighting).GetTypeInfo().Assembly.GetTypes())
            {
                var attribute = type.GetCustomAttribute<HighlightingItemAttribute>();
                if (attribute != null)
                {
                    menu.AddItem(new GUIContent(attribute.menuItem), false, delegate
                    {
                        ReplaceHighlighting(type);
                    });
                }
            }
            menu.ShowAsContext();
        }

        private void ReplaceHighlighting(Type component)
        {
            if (m_MouseTrigger.highlighting != null)
            {
                DestroyImmediate(m_MouseTrigger.highlighting);
            }

            m_MouseTrigger.highlighting = gameObjectCache.AddComponent(component) as Highlighting;
        }
    }
}
