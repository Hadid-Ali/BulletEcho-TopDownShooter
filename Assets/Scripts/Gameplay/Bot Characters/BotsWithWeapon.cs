using System.Collections;
using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;

public class BotsWithWeapon : NavigationAgent
{
   [SerializeField] private SimpleWeapon m_EnemyWeapon;
   [SerializeField] private float m_BotAttackRate;

   protected override void Init()
   {
      base.Init();
      
      m_EnemyWeapon.Initialize(ApplyDamageWithWeapon);
      m_EnemyWeapon.SetWeaponShootingRate(m_BotAttackRate);
      m_AnimatorController.SetHasWeapon(true);
   }

   public override void AttackState()
   {
      base.AttackState();
      m_AnimatorController.ShootWeapon(m_EnemyWeapon.Fire);
   }

   private void ApplyDamageWithWeapon(float damage)
   {
      if(m_Target is null || !m_BotLook.CheckUnderView(m_Target))
         return;
      
      if (m_Target.TryGetComponent(out HealthController healthController))
      {
         healthController.ApplyDamage(damage);
      }
   }

   protected override void OnSwitchToAttack()
   {
      base.OnSwitchToAttack();
      m_AnimatorController.SetAimPose(true);
   }

   protected override void OnSwitchToChase()
   {
      base.OnSwitchToChase();
      m_AnimatorController.SetAimPose(false);
   }
}
