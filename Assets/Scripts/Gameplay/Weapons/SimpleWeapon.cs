using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Weapons;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleWeapon : BaseWeapon
{
    private Action<float> m_WeaponTryDamage;

    public void Initialize(Action<float> OnWeaponTryDamage)
    {
        m_WeaponTryDamage = OnWeaponTryDamage;
    }


    public void OnDisable()
    {
        m_WeaponTryDamage = null;
    }
    
    protected override void FireInternal()
    {
        if (m_TotleBullets <= 0 && m_IsPlayer)
        {
            GameEvents.GameplayEvents.SpecialWeapon.Raise(global::WeaponName.Pistol, 0);
        }

        if (m_IsPlayer && m_CurrentAmmoCount <= 0)
        {
            return;
        }
        base.FireInternal();
        m_WeaponTryDamage?.Invoke(m_WeaponDamage);
    }

    public void IncreaseBullets(int ammoCount)
    {
        if (m_IsPlayer)
        {
            m_TotleBullets += ammoCount;
            UpdateBullet();
        }
    }
}
