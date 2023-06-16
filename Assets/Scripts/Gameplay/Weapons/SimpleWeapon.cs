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
        if (m_TotleBullets <= 0 && m_WeaponName != global::WeaponName.Pistol)
        {
            print("BUllets count"+m_TotleBullets);
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
        print("Ya a raha ha increase bullets");
        if (m_IsPlayer)
        {
            print("Ya a raha ha increase bullets player");
            m_TotleBullets += ammoCount;
            UpdateBullet();
        }
    }
}
