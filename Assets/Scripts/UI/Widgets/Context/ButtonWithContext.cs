using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonWithContext : MonoBehaviour
{
   [SerializeField] protected Button m_ButtonComponent;
   [SerializeField] private Image m_ButtonImageComponent;
   
   private Action<UIContext> m_ButtonAction;
   private UIContext m_Context;

   private void Start()
   {
      InitializeInternal();
   }

   private void InitializeInternal()
   {
      SubscribeClickInternal(() => m_ButtonAction?.Invoke(m_Context));
   }

   public virtual void SetContext(UIContext context)
   {
      m_Context = context;
      SetSprite(context.Sprite);
   }
   
   public void SubscribeEvent(Action<UIContext> action)
   {
      m_ButtonAction = action;
   }
   
   private void SetSprite(Sprite sprite)
   {
      m_ButtonImageComponent.sprite = sprite;
   }

   protected void SubscribeClickInternal(Action action)
   {
      m_ButtonComponent.onClick.AddListener(() => { action(); });
   }
}
