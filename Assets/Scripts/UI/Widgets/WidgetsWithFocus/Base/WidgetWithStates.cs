using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    [Serializable]
    public class WidgetWithStates
    {
        [SerializeField] protected Image m_Image;
    
        [SerializeField] private Sprite m_NormalState;
        [SerializeField] private Sprite m_FocusState;

        [SerializeField] private List<GameObject> m_OnFocusObjects;

        public virtual void Focus()
        {
            SetSpriteInternal(m_FocusState);
            ToggleFocusObjects(true);
        }

        public virtual void UnFocus()
        {
            SetSpriteInternal(m_NormalState);
            ToggleFocusObjects(false);
        }

        private void ToggleFocusObjects(bool toggle)
        {
            for (int i = 0; i < m_OnFocusObjects.Count; i++)
            {
                m_OnFocusObjects[i].SetActive(toggle);
            }
        }
        
        private void SetSpriteInternal(Sprite sprite)
        {
            if(m_Image is null || sprite is null)
                return;

            m_Image.sprite = sprite;
        }
    }
}