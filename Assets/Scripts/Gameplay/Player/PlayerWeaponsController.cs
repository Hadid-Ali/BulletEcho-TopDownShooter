using System;
using UnityEngine;

public class PlayerWeaponsController : MonoBehaviour
{
   [SerializeField] private SimpleWeapon m_CurrentPrimaryWeapon;
   [SerializeField] private WeaponAimingComponent m_AimingComponent;
   [SerializeField] private PlayerAnimatorController m_AnimatorController;

   [SerializeField] private float m_MaximumShootingAngle = 26f;
      
   private AimObject m_CurrentAimObject;
   private Transform m_Transform;
   
   private bool m_HasTarget = false;

   private void Start()
   {
      m_Transform = transform;
   }

   private void OnEnable()
   {
      m_CurrentPrimaryWeapon.Initialize(TryWeaponDamage);
      m_AimingComponent.Initialize(SetAimObject);
   }

   private void Update()
   {
      FiringMechanism();
   }

   private void TryWeaponDamage(float damage)
   {
      if(m_CurrentAimObject is null)
         return;

      Debug.LogError($"Try Weapon Damage {damage}");
      if (m_CurrentAimObject.Target.TryGetComponent(out HealthController healthController))
      {
         healthController.ApplyDamage(damage);
      }
   }

   private void SetAimObject(AimObject aimObject)
   {
      m_CurrentAimObject = aimObject;
      m_HasTarget = aimObject != null;
      
      SetAimingEnabled(m_HasTarget);
   }

   private void FiringMechanism()
   {
      bool canFire = m_HasTarget;
      if (m_HasTarget)
      {
         Vector3 aimDirection = m_CurrentAimObject.Target.transform.position - m_Transform.position;
         Vector3 direction = transform.forward;

         float angle = Vector3.Angle(aimDirection, direction);
         canFire = angle <= m_MaximumShootingAngle;
      }
      SetFiringEnabled(canFire);
   }
   
   private void SetFiringEnabled(bool enable)
   {
      m_CurrentPrimaryWeapon.SetFiringEnabled(enable);
   }

   private void SetAimingEnabled(bool enable)
   {
      m_AnimatorController.SetAimPose(enable);
   }
}
