using UnityEngine;

namespace UI
{
    public class Crosshair : MonoBehaviour
    {
        [SerializeField] private WidgetStatesWithScale m_CrosshairState;
        
        public Vector3 AimingPosition => new Vector3(Screen.width / 2f, Screen.height / 2f, 0f);

        public void Focus()
        {
            m_CrosshairState.Focus();
        }

        public void UnFocus()
        {
            m_CrosshairState.UnFocus();
        }
    }
}
