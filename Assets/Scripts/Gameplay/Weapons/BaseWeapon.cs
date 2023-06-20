using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

namespace Gameplay.Weapons
{
    public abstract class BaseWeapon : MonoBehaviour
    {
        [Header("Name")]
        
        [SerializeField] protected WeaponName m_WeaponName;

        [Header("Components")]
        
        [SerializeField]
        protected AnimatorOverrideController m_WeaponAnimator;

        [SerializeField] protected WeaponVFXHandler m_WeaponVFXHandler;
        [SerializeField] protected WeaponSFXHandler m_WeaponSfxHandler;

        [Header("Shooting Properties")]
        
        [SerializeField] protected float m_WeaponDamage;
        [SerializeField] private float m_WeaponShootingRate = 0.2f;
        [SerializeField] protected int m_WeaponCoolDownDuration;

        [Header("Bullet Properties")]
        
        [SerializeField] protected int m_TotleBullets;
        [SerializeField] protected int m_MagazineSize;
        [SerializeField] private WeaponReloadMode m_WeaponReloadMode = WeaponReloadMode.UNLIMITED_AMMO;
        
        protected int m_CurrentAmmoCount = 0;

        private float m_CurrentShotTime;
        private bool m_Fire = false;

        public Action<int> OnWeaponRemainingCoolDownUpdate;
        public Action<int> OnWeaponAmmoCountUpdate;

        protected AimObject m_CurrentAimObject;
        private WaitForSeconds m_WeaponCooldownRoutineWait = new(1f);
        private WaitForEndOfFrame m_FrameDelay = new();
        
        public WeaponName WeaponName => m_WeaponName;
        public AnimatorOverrideController WeaponAnimator => m_WeaponAnimator;

        private bool HasBullets => m_CurrentAmmoCount > 0;
        private bool HasAmmo => m_TotleBullets > 0;

        private bool IsLimitedAmmoStocked =>
            m_WeaponReloadMode is WeaponReloadMode.INSTANT_RELOAD or WeaponReloadMode.COOLWDOWNBASED_RELOAD;

        private void Start()
        {
            ReloadWeaponInternal();
        }

        private void Update()
        {
            if (!m_Fire || !HasBullets) return;
            Fire();
        }

        public void RegisterAimObject(AimObject aimObject)
        {
            m_CurrentAimObject = aimObject;
        }

        public void SetFiringEnabled(bool firing)
        {
            SetFiringEnabledInternal(firing);
        }

        private void SetFiringEnabledInternal(bool firing)
        {
            m_Fire = firing;
        }

        public void SetWeaponShootingRate(float shootingRate)
        {
            m_WeaponShootingRate = shootingRate;
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

            ConsumeBullet();
        }

        private void ConsumeBullet()
        {
            if (m_WeaponReloadMode is WeaponReloadMode.UNLIMITED_AMMO)
                return;
            
            m_CurrentAmmoCount--;
            OnBulletConsumed();
        }

        private void OnBulletConsumed()
        {
            if (m_CurrentAmmoCount <= 0)
            {
                SetFiringEnabled(false);

                if (!HasAmmo && IsLimitedAmmoStocked)
                {
                    WeaponRunOutOfAmmo();
                    return;
                }
                
                switch (m_WeaponReloadMode)
                {
                    case WeaponReloadMode.INSTANT_RELOAD:
                        ReloadWeapon(true);
                        break;
                    
                    default:
                        StartCoroutine(WeaponCoolDownRoutine(m_WeaponReloadMode is WeaponReloadMode.COOLWDOWNBASED_RELOAD));
                        break;
                }
            }
        }

        protected virtual void WeaponRunOutOfAmmo()
        {
            
        }
        
        private IEnumerator WeaponCoolDownRoutine(bool shouldConsumeMagazine)
        {
            float currentReloadingInstant = 0;
            
            while (currentReloadingInstant < m_WeaponCoolDownDuration)
            {
                yield return m_FrameDelay;
                currentReloadingInstant += Time.deltaTime;
            }
            ReloadWeapon(shouldConsumeMagazine);
        }
        
        protected virtual void ReloadWeapon(bool consumeMagazine)
        {
            if (consumeMagazine)
                m_TotleBullets -= m_MagazineSize;

            ReloadWeaponInternal();
            OnWeaponReloaded();
        }

        private void ReloadWeaponInternal()
        {
            m_CurrentAmmoCount = m_MagazineSize;
        }

        private void OnWeaponReloaded()
        {
            SetFiringEnabled(true);
        }

        protected void UpdateBullet()
        {
            StartCoroutine(WeaponCoolDownRoutine(true));
        }
    }
}