using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion : MonoBehaviour
{
   [SerializeField] private LayerMask m_ExplosionMask;
   [SerializeField] private float m_ImpactRadius = 5f;

   [SerializeField] private float m_Damage = 25f;
   private Collider[] nearbyColliders;
   
   private void Awake()
   {
      ApplyDamage();
   }

   protected virtual void ApplyDamage()
   {
      nearbyColliders = Physics.OverlapSphere(transform.position, m_ImpactRadius, m_ExplosionMask);

      if (nearbyColliders.Length < 0)
         return;

      for (int i = 0; i < nearbyColliders.Length; i++)
      {
         if(nearbyColliders[i].gameObject is null)
            continue;
         
         if (nearbyColliders[i].TryGetComponent(out HealthController healthController))
         {
            healthController.ApplyDamage(m_Damage);
         }
      }
   }
}
