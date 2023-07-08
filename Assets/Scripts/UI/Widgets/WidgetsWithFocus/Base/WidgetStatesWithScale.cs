using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class WidgetStatesWithScale : WidgetWithStatesAndTint
    {
        [SerializeField] private float m_NormalScale;
        [SerializeField] private float m_FocusScale;

        public override void Focus()
        {
            base.Focus();
            SetScaleInternal(m_FocusScale);
        }

        public override void UnFocus()
        {
            base.UnFocus();
            SetScaleInternal(m_NormalScale);
        }

        private void SetScaleInternal(float scale)
        {
            if (m_Image is null)
                return;

            m_Image.rectTransform.localScale = new Vector3(scale, scale, scale);
        }
    }
}
