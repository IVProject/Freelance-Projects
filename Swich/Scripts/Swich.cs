using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace InvsoftEngine.UI
{
    [RequireComponent(typeof(Image))]
    public class Swich: Selectable, IPointerClickHandler, ISubmitHandler, ICanvasElement
    {
        [System.Serializable]
        public class SwichEvent : UnityEvent<bool> { }

        [SerializeField] private RectTransform m_FillRect;
        [SerializeField] private RectTransform m_HandleRect;
        [SerializeField] private bool m_Value;
        [Space(10)]
        [SerializeField] private SwichEvent m_OnChangeValue = new SwichEvent();

        private DrivenRectTransformTracker m_Tracker;
        private bool m_DelayedUpdateVisuals = false;

        protected override void OnEnable()
        {
            base.OnEnable();
            value = m_Value;
            UpdateVisuals();
        }

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            if (IsActive())
            {
                m_DelayedUpdateVisuals = true;
            }
            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
                CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
        }
#endif

        public void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
                onChangeValue.Invoke(value);
#endif
        }

        public void LayoutComplete()
        {}

        public void GraphicUpdateComplete()
        {}

        public void OnPointerClick(PointerEventData eventData)
        {
            value = !value;
        }

        public void OnSubmit(BaseEventData eventData)
        {
            value = !value;
        }

        protected virtual void Update()
        {
            if(m_DelayedUpdateVisuals)
            {
                m_DelayedUpdateVisuals = false;
                value = m_Value;
            }
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            m_Tracker.Clear();
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
            set { m_OnChangeValue = value; }
        }

        private void SetFillPosition(float value)
        {
            if (m_FillRect != null)
            {
                m_Tracker.Add(this, m_FillRect, DrivenTransformProperties.Anchors);
                m_FillRect.anchorMin = Vector2.zero;
                m_FillRect.anchorMax = new Vector2(Mathf.Clamp(value, 0, 1), 1);
            }
        }

        private void SetHandlePosition(float value)
        {
            if (m_HandleRect != null)
            {
                m_Tracker.Add(this, m_HandleRect, DrivenTransformProperties.Anchors);
                Vector2 anchorMin = Vector2.zero;
                Vector2 anchorMax = Vector2.one;
                anchorMin[0] = anchorMax[0] = (Mathf.Clamp(value, 0, 1));
                m_HandleRect.anchorMin = anchorMin;
                m_HandleRect.anchorMax = anchorMax;
              
            }
        }

        private void UpdateVisuals()
        {
            float target = value ? 1 : 0;
            m_Tracker.Clear();
            SetFillPosition(target);
            SetHandlePosition(target);
        }

    }
}
