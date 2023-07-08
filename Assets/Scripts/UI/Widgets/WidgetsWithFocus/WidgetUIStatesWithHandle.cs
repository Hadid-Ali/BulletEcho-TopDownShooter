using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI
{
    public class WidgetUIStatesWithHandle : WidgetUIStates
    {
        [SerializeField] private WidgetWithStates[] m_ExtraWidgetWithStates;
        
        public override void FocusWidget()
        {
            base.FocusWidget();
            for (int i = 0; i < m_ExtraWidgetWithStates.Length; i++)
            {
                m_ExtraWidgetWithStates[i].Focus();
            }
        }

        public override void UnFocusWidget()
        {
            base.UnFocusWidget();
            for (int i = 0; i < m_ExtraWidgetWithStates.Length; i++)
            {
                m_ExtraWidgetWithStates[i].UnFocus();
            }
        }
    }
}
