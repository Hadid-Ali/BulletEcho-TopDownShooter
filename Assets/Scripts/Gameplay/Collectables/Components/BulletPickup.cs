using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPickup : Collectable
{
    [SerializeField] private int m_BulletsCount = 10;
    [SerializeField] private WeaponName m_WeaponName;
    
    public override void Collect()
    {
        base.Collect();
        GameEvents.GameplayEvents.SwitchWeaponWithBullets.Raise(m_WeaponName, m_BulletsCount);
    }
}
