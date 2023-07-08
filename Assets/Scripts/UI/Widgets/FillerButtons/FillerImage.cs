using System;
using System.Collections;
using UnityEngine.UI;
using UnityEngine;

public class FillerImage : MonoBehaviour
{
   [SerializeField] private Image m_FillerImage;

   private float m_FillDuration = 1f;
   
   private GameEvent m_OnFilled = new();
   private GameEvent m_OnEmptied = new();

   private WaitForEndOfFrame m_FrameWait = new();

   protected bool IsFilled => m_FillerImage.fillAmount >= 1;
   protected bool IsEmpty => m_FillerImage.fillAmount <= 0;
   
   public void Initialize(Action onFilled, Action onEmpty, float duration)
   {
      m_FillDuration = duration;
      m_OnFilled.Register(onFilled);
      m_OnEmptied.Register(onEmpty);
   }

   /// <summary>
   /// Filling Amount Between 0 and 1
   /// </summary>
   /// <param name="fillAmount"></param>
   public void Consume(float fillAmount = 1f)
   {
      fillAmount = Mathf.Clamp01(fillAmount);
      SetFillAmount(1 - fillAmount);
   }

   private void SetFillAmount(float value)
   {
      m_FillerImage.fillAmount = value;

      if (IsFilled)
      {
         OnFillCompletely();
      }
      else if (IsEmpty)
      {
         OnEmptyFiller();
      }
   }

   private void OnFillCompletely()
   {
      m_OnFilled.Raise();
   }

   private void OnEmptyFiller()
   {
      m_OnEmptied.Raise();
      StartCoroutine(FillRoutine());
   }

   private IEnumerator FillRoutine()
   {
      float value = m_FillerImage.fillAmount;

      while (value <= 1)
      {
         value += Time.deltaTime;
         SetFillAmount(value);

         yield return m_FrameWait;
      }
   }
}
