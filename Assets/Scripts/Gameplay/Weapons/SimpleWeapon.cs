using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Weapons;
using Unity.VisualScripting;
using UnityEngine;

public class SimpleWeapon : BaseWeapon
{
    private Action<float> m_WeaponTryDamage;
    private Action m_OnWeaponRunOutOfAmmo;
    
    public void Initialize(Action<float> OnWeaponTryDamage)
    {
        m_WeaponTryDamage = OnWeaponTryDamage;
    }

    public void OnDisable()
    {
        m_WeaponTryDamage = null;
    }

    protected override void WeaponRunOutOfAmmo()
    {
        base.WeaponRunOutOfAmmo();
        GameEvents.GameplayEvents.SwitchWeaponWithBullets.Raise(WeaponName.Pistol, 0);
    }

    protected override void FireInternal()
    {
        base.FireInternal();
        m_WeaponTryDamage?.Invoke(m_WeaponDamage);
    }

    public void IncreaseBullets(int ammoCount)
    {
        m_TotleBullets += ammoCount;
        UpdateBullet();
    }
}
