using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace InvsoftEngine.UI
{
    [RequireComponent(typeof(Image))]
    public class ProgressBar : MonoBehaviour
    {
        [System.Serializable]
        public class ProgressBarEvents: UnityEvent<float> { }

        [Range(0, 1)]
        [SerializeField] private float m_Progress;
        [SerializeField] private Image.FillMethod m_FillMethod;
        [SerializeField] private bool m_TextVisibility;
        [Space()]
        [SerializeField] private Image m_Fill;
        [SerializeField] private Text m_Text;
        [Space(10)]
        [SerializeField] private ProgressBarEvents m_OnChangeProgress = new ProgressBarEvents();

        private void OnValidate()
        {
            progress = m_Progress;
            textVisibility = m_TextVisibility;
            fillMethod = m_FillMethod;
        }

        public float progress
        {
            get { return m_Progress; }
            set
            {
                if (m_Fill != null)
                {
                    m_Fill.fillAmount = value;
                    m_Progress = value;
                    m_OnChangeProgress.Invoke(value);
                }
                if (m_Text != null)
                {
                    m_Text.text = "" + Mathf.Round(value * 100) + "%";
                }
            }
        }

        public bool textVisibility
        {
            get { return m_TextVisibility; }
            set
            {
                if (m_Text != null)
                {
                    m_TextVisibility = value;
                    m_Text.gameObject.SetActive(value);
                }
            }
        }

        public Image.FillMethod fillMethod
        {
            set
            {
                m_FillMethod = value;
                if (m_Fill != null)
                {
                    m_Fill.fillMethod = value;
                }
            }

        }

        public Image imageFill
        {
            set
            {
                m_Fill = value;
            }
        }

        public Text text
        {
            set
            {
                m_Text = value;
            }
        }

        public ProgressBarEvents onChangeProgress
        {
            get { return m_OnChangeProgress; }
        }
    }
}
