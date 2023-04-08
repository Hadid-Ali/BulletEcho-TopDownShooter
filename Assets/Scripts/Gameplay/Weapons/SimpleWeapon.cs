using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Weapons;
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
        base.FireInternal();
        m_WeaponTryDamage?.Invoke(m_WeaponDamage);
    }
}
