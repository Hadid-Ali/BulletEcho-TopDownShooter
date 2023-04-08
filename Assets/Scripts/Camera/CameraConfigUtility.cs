using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraConfigUtility : MonoBehaviour
{
   [SerializeField] private CameraConfig m_ConfigToApply;

   [ContextMenu("Apply Config")]
   public void ApplyConfig()
   {
     //  CameraManager.Instance.ApplyCameraConfig(m_ConfigToApply);
   }
}
