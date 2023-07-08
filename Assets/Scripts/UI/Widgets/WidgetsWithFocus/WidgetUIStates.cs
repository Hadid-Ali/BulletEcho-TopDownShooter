using UnityEngine;
using UnityEngine.EventSystems;

namespace UI
{
   public class WidgetUIStates : MonoBehaviour,IPointerDownHandler,IPointerUpHandler
   {
      [SerializeField] private WidgetWithStates m_WidgetWithStates;

      void IPointerDownHandler.OnPointerDown(PointerEventData eventData)
      {
         FocusWidget();
      }

      void IPointerUpHandler.OnPointerUp(PointerEventData eventData)
      {
         UnFocusWidget();
      }

      public virtual void FocusWidget()
      {
         m_WidgetWithStates.Focus();
      }

      public virtual void UnFocusWidget()
      {
         m_WidgetWithStates.Focus();
      }
   }
}
