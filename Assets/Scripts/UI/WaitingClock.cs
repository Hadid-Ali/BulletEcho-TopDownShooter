using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaitingClock : MonoBehaviour
{
   [Header("Components")]
   
   [SerializeField] private GameObject m_Container;
   [SerializeField] private Image m_TimerImage;

   [Header("Values")] 
   
   [SerializeField] private float m_ClockTimerWait = 3f;

   private Action m_OnTimerComplete;
   private WaitForEndOfFrame m_FrameDelay = new();
   private Coroutine m_FillRoutine = null;
   
   private void OnEnable()
   {
      GameEvents.GameplayEvents.WaitForAction.Register(OnWaitForAction);
      GameEvents.GameplayEvents.CancelWaitForAction.Register(OnCancel);
   }

   private void OnDisable()
   {
      GameEvents.GameplayEvents.WaitForAction.Unregister(OnWaitForAction);
      GameEvents.GameplayEvents.CancelWaitForAction.Unregister(OnCancel);
   }

   private void OnWaitForAction(Action action)
   {
      m_OnTimerComplete = action;
      SetContainerActiveState(true);
      m_FillRoutine = StartCoroutine(ClockFillRoutine());
   }
   
   private void OnCancel()
   {
      StopCoroutine(m_FillRoutine);
      ResetRoutineData();
   }

   private void ResetRoutineData()
   {
      m_FillRoutine = null;
      m_OnTimerComplete = null;
      
      m_Container.SetActive(false);
      m_TimerImage.fillAmount = 0;
   }

   private void SetContainerActiveState(bool status)
   {
      m_Container.SetActive(status);
   }
   
   private void OnRoutineCompleted()
   {
      m_OnTimerComplete.Invoke();
      ResetRoutineData();
   }

   private IEnumerator ClockFillRoutine()
   {
      float fillAmount = Time.deltaTime / m_ClockTimerWait;

      while (m_TimerImage.fillAmount < 1f)
      {
         m_TimerImage.fillAmount += fillAmount;
         yield return m_FrameDelay;
      }
      OnRoutineCompleted();
   }
}
