using UnityEngine;

namespace Proekt.HighlightingManagement
{
    #region Attributes
    [HighlightingItem("Highlighting Emission")]
    [AddComponentMenu("Highlighting Management/Highlightings/Highlighting Emission")]
    #endregion
    public class HighlightingEmission : Highlighting
    {
        [SerializeField] private Color m_Color = Color.red;

        private int m_EmissionID;
        private Renderer m_Render;

        public Color color { get => m_Color; set => m_Color = value; }

        private void Awake()
        { 
            m_EmissionID = Shader.PropertyToID("_EmissionColor");
            m_Render = GetComponent<Renderer>();
        }

        public override void Highlight()
        {
            m_Render.material.SetColor(m_EmissionID, m_Color);
            m_Render.material.EnableKeyword("_EMISSION");
        }

        public override void Normal()
        {
            m_Render.material.DisableKeyword("_EMISSION");
        }
    }
}
