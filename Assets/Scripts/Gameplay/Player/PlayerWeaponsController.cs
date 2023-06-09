using System;
using IndieMarc.EnemyVision;
using UnityEngine;

public class PlayerWeaponsController : MonoBehaviour
{
   [Header("Components")]
   
   [SerializeField] private SimpleWeapon m_CurrentPrimaryWeapon;
   [SerializeField] private WeaponAimingComponent m_AimingComponent;
   [SerializeField] private PlayerAnimatorController m_AnimatorController;
   [SerializeField] private VisionCone m_AimingCone;

   [Header("Values")]
   [SerializeField] private float m_MaximumShootingAngle = 26f;
      
   private AimObject m_CurrentAimObject;
   private Transform m_Transform;

   private HealthController m_CurrentTargetHealthController;
   
   private bool m_HasTarget = false;

   private void Start()
   {
      m_Transform = transform;
      SetAimingAngle(m_MaximumShootingAngle);
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

   private void SetAimingAngle(float value)
   {
       m_MaximumShootingAngle = value;
       m_AimingCone.vision_angle = value * 2;
   }

   private void TryWeaponDamage(float damage)
   {
      if (m_CurrentAimObject is null)
         return;

      m_CurrentTargetHealthController.ApplyDamage(damage);
   }

   private void SetAimObject(AimObject aimObject)
   {
      m_CurrentAimObject = aimObject;
      m_HasTarget = aimObject != null;
      
      m_CurrentTargetHealthController = aimObject ? aimObject.GetComponent<HealthController>() : null;
      
      bool canAim = m_HasTarget;
      if (m_CurrentTargetHealthController != null)
      {
         canAim &= m_CurrentTargetHealthController.IsAlive;
      }
      SetAimingEnabled(canAim);
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
         
         Debug.LogError(angle);
      }

      if (canFire && m_CurrentTargetHealthController != null)
      {
         bool isTargetAlive = m_CurrentTargetHealthController.IsAlive;
         canFire = isTargetAlive;

         if (!isTargetAlive)
         {
            SetAimObject(null);
         }
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
