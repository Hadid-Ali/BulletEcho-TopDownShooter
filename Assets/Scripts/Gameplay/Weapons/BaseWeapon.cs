using System;
using System.Collections;
using UnityEngine;

namespace Gameplay.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {     
        [SerializeField] protected WeaponVFXHandler m_WeaponVFXHandler;
        [SerializeField] protected WeaponSFXHandler m_WeaponSfxHandler;
     
        [SerializeField] protected float m_WeaponDamage;
        
        [SerializeField] protected int m_MagazineSize;
        [SerializeField] protected int m_WeaponCoolDownDuration;   
        
        [SerializeField] private float m_WeaponShootingRate = 0.2f;

        private int m_CurrentAmmoCount;
        private int m_CurrentCoolDownRemainingTime;
    
        private float m_CurrentShotTime;
        private bool m_Fire = false;

        public Action<int> OnWeaponRemainingCoolDownUpdate;
        public Action<int> OnWeaponAmmoCountUpdate;

        protected AimObject m_CurrentAimObject;
        private WaitForSeconds m_WeaponCooldownRoutineWait = new WaitForSeconds(1f);

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
            if(!m_Fire)
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
            if (m_CurrentAmmoCount <= 0)
            {
                m_CurrentCoolDownRemainingTime = m_WeaponCoolDownDuration;
            }
        }

        private IEnumerator WeaponCoolDownRoutine()
        {
            while (m_CurrentCoolDownRemainingTime > 0)
            {
                OnWeaponRemainingCoolDownUpdate?.Invoke(m_CurrentCoolDownRemainingTime);
                yield return m_WeaponCooldownRoutineWait;

                m_CurrentCoolDownRemainingTime--;
            }

            m_CurrentAmmoCount = m_MagazineSize;
        }
    }
}