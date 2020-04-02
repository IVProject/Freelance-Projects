using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace InvsoftEngine.UI
{
    [RequireComponent(typeof(Image))]
    public class Swich: Selectable, IPointerClickHandler, ISubmitHandler
    {
        [System.Serializable]
        public class SwichEvent : UnityEvent<bool> { }

        [SerializeField] private RectTransform m_FillRect;
        [SerializeField] private RectTransform m_HandleRect;
        [SerializeField] private bool m_Value;
        [Space(10)]
        [SerializeField] private SwichEvent m_OnChangeValue = new SwichEvent();

        private DrivenRectTransformTracker m_Tracker;
        private float currentPosition;

        protected override void OnValidate()
        {
            base.OnValidate();
            value = m_Value;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            value = !value;
        }

        public void OnSubmit(BaseEventData eventData)
        {
            value = !value;
        }
        
        public RectTransform fillRect
        {
            set { m_FillRect = value; }
        }

        public RectTransform handleRect
        {
            set { m_HandleRect = value; }
        }


        public bool value
        {
            get { return m_Value; }
            set
            {
                m_Value = value;
                m_OnChangeValue.Invoke(m_Value);
                UpdateVisuals();
            }
        }

        public SwichEvent onChangeValue
        {
            get { return m_OnChangeValue; }
        }

        private void SetFillPosition(float value)
        {
            m_Tracker.Clear();

            if (m_FillRect != null)
            {
                m_Tracker.Add(this, m_FillRect, DrivenTransformProperties.Anchors);
                m_FillRect.anchorMin = Vector2.zero;
                m_FillRect.anchorMax = new Vector2(Mathf.Clamp(value, 0, 1), 1);
            }
        }

        private void SetHandlePosition(float value)
        {
            m_Tracker.Clear();

            if (m_HandleRect != null)
            {
                m_Tracker.Add(this, m_HandleRect, DrivenTransformProperties.Anchors);
                m_HandleRect.anchorMin = new Vector2(Mathf.Clamp(value, 0, 1), 0);
                m_HandleRect.anchorMax = new Vector2(Mathf.Clamp(value, 0, 1), 1);
            }
        }

        private void UpdateVisuals()
        {
            float target = value ? 1 : 0;
            SetFillPosition(target);
            SetHandlePosition(target);
        }
    }
}
