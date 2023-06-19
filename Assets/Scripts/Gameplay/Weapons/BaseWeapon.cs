using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;

namespace Gameplay.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [SerializeField] protected WeaponName m_WeaponName;
        [SerializeField] protected AnimatorOverrideController m_WeaponAnimator;
        [SerializeField] protected WeaponVFXHandler m_WeaponVFXHandler;
        [SerializeField] protected WeaponSFXHandler m_WeaponSfxHandler;
     
        [SerializeField] protected float m_WeaponDamage;
        
        [SerializeField] protected int m_TotleBullets;
        [SerializeField] protected int m_MagazineSize;
        [SerializeField] protected int m_WeaponCoolDownDuration;   
        [SerializeField] protected bool m_IsPlayer = false;

        [SerializeField] private float m_WeaponShootingRate = 0.2f;

        protected int m_CurrentAmmoCount = 0;
        private int m_CurrentCoolDownRemainingTime;
    
        private float m_CurrentShotTime;
        private bool m_Fire = false;

        public Action<int> OnWeaponRemainingCoolDownUpdate;
        public Action<int> OnWeaponAmmoCountUpdate;

        protected AimObject m_CurrentAimObject;
        private WaitForSeconds m_WeaponCooldownRoutineWait = new WaitForSeconds(1f);

        private void Start()
        {
            
        }

        public void RegisterAimObject(AimObject aimObject)
        {
            m_CurrentAimObject = aimObject;
        }

        public void SetFiringEnabled(bool firing)
        {
            m_Fire = firing;
        }
        
        public void SetWeaponShootingRate(float shootingRate)
        {
            m_WeaponShootingRate = shootingRate;
        }

        private void Update()
        {
            if (!m_Fire)
                return;
            Fire();
        }

        public void Fire()
        {
            if (Time.time > m_CurrentShotTime)
            {
                m_CurrentShotTime = m_WeaponShootingRate + Time.time;
                FireInternal();
            }
        }

        public void Fire(Action action)
        {
            
        }
    
        protected virtual void FireInternal()
        {
            m_WeaponVFXHandler.ShowMuzzleEffects();
            m_WeaponSfxHandler.ShootSound();
        
            m_CurrentAmmoCount--;
            Debug.LogError($"m_WeaponAmmoCount {m_CurrentAmmoCount}");
            if (m_CurrentAmmoCount <= 0 && m_IsPlayer)
            {
                SetFiringEnabled(false);
                m_CurrentCoolDownRemainingTime = m_WeaponCoolDownDuration;
                if (m_IsPlayer)
                {
                    m_TotleBullets -= m_MagazineSize;
                }
                StartCoroutine(WeaponCoolDownRoutine());
            }
        }

        private IEnumerator WeaponCoolDownRoutine()
        {
            // while (m_CurrentCoolDownRemainingTime > 0)
            // {
            //     OnWeaponRemainingCoolDownUpdate?.Invoke(m_CurrentCoolDownRemainingTime);
            //     yield return m_WeaponCooldownRoutineWait;
            //
            //     m_CurrentCoolDownRemainingTime--;
            // }

            yield return new WaitForSeconds(m_WeaponCoolDownDuration);

            m_CurrentAmmoCount = m_MagazineSize;
            SetFiringEnabled(true);
            Debug.LogError($"m_WeaponAmmoCount {m_CurrentAmmoCount}");

        }

        public void UpdateBullet()
        {
            StartCoroutine(WeaponCoolDownRoutine());
        }
        public WeaponName WeaponName() => m_WeaponName;
        
        public AnimatorOverrideController GetWeaponAnimator() => m_WeaponAnimator;

    }
}