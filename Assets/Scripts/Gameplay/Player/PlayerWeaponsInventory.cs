using System;
using System.Collections;
using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;

public class PlayerWeaponsInventory : MonoBehaviour
{
    [SerializeField] private CharacterAnimatorController m_CraracterAnimatorController;
    [SerializeField] private List<BaseWeapon> m_Weapons;

    private void OnEnable()
    {
        GameEvents.GameplayEvents.SpecialWeapon.Register(EquipWeapon);
    }
    private void OnDisable()
    {
        GameEvents.GameplayEvents.SpecialWeapon.UnRegister(EquipWeapon);
    }
    public int ToEquipWeapon(WeaponName WeaponName) => m_Weapons.FindIndex(v => v.WeaponName() == WeaponName);
    public int GetLastActiveWeapon() => m_Weapons.FindIndex(v => v.gameObject.activeSelf);
    void EquipWeapon(WeaponName WeaponName, int bulletCount)
    {
        int EquipWeaponIndex = ToEquipWeapon(WeaponName);
        m_Weapons[GetLastActiveWeapon()].gameObject.SetActive(false);
        m_Weapons[EquipWeaponIndex].gameObject.SetActive(true);
        GameEvents.GameplayEvents.EquipWeapon.Raise((SimpleWeapon)m_Weapons[EquipWeaponIndex]);
        SimpleWeapon Weapon = (SimpleWeapon)m_Weapons[EquipWeaponIndex];
        Weapon.IncreaseBullets(bulletCount);
        m_CraracterAnimatorController.ApplyAnimatorOverrideController(m_Weapons[EquipWeaponIndex].GetWeaponAnimator());
    }
}
