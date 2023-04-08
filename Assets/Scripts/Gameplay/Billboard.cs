using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
   private Transform m_Transform;
   private Transform m_BillBoardTarget;

   private void Start()
   {
      m_Transform = transform;
   }

   private void Update()
   {
//      if(GameManager.Instance.GameplayManager.GlobalBillBoardTarget is null)
         return;
      
      m_Transform.LookAt(GameManager.Instance.GameplayManager.GlobalBillBoardTarget);
      m_Transform.eulerAngles.Set(0, m_Transform.eulerAngles.y, 0);
   }
}
