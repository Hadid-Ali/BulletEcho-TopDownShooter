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
}
