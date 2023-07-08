using System;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    [Serializable]
    public class WidgetWithStatesAndTint : WidgetWithStates
    {
        [SerializeField] private Color m_NormalColor;
        [SerializeField] private Color m_FocusColor;

        public override void Focus()
        {
            base.Focus();
            SetColorInternal(m_FocusColor);
        }

        public override void UnFocus()
        {
            base.UnFocus();
            SetColorInternal(m_NormalColor);
        }

        private void SetColorInternal(Color color)
        {
            if (m_Image is null)
                return;
            m_Image.color = color;
        }
    }
}