using System.Collections;
using System.Collections.Generic;
using Gameplay.Weapons;
using UnityEngine;

public class WeaponWithAttachedAnimators : BaseWeapon
{
   [SerializeField] private Animator m_Animator;

   public Animator AttachedAnimator => m_Animator;
}
