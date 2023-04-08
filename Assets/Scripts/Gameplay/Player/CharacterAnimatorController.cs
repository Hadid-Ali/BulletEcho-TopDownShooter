using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;

public abstract class CharacterAnimatorController : MonoBehaviour
{
    [SerializeField] protected Animator m_Animator;
    public Animator Animator => m_Animator;

    private int m_SpeedAnimatorParameter = UnityEngine.Animator.StringToHash("Speed");
    
    private readonly int m_HasWeaponParameter = Animator.StringToHash("HasWeapon");
    private readonly int m_AimWeaponParamter = Animator.StringToHash("IsAiming");
    private readonly int m_ShootWeaponParameter = Animator.StringToHash("Shoot");
    
        private readonly int m_DeadParameter = Animator.StringToHash("Dead");
    private readonly int m_DieParameter = Animator.StringToHash("Die");
    private readonly int m_DamageParameter = Animator.StringToHash("GetDamage");

    private Action m_OnShootingPoseAction;
    
    public void ApplyAnimatorOverrideController(AnimatorOverrideController animatorOverrideController,Avatar avatar = null)
    {
        m_Animator.runtimeAnimatorController = animatorOverrideController;
        if(avatar is null)
            return;
        m_Animator.avatar = avatar;
    }
    
    public void SetIdle()
    {
        SetSpeed(0f);
    }
    
    public void SetDamagePose()
    {
        m_Animator.SetTrigger(m_DamageParameter);
    }

    public void DiePose()
    {
        m_Animator.SetTrigger(m_DieParameter);
        m_Animator.SetBool(m_DeadParameter, true);
    }

    public void SetSpeed(float value)
    {
        m_Animator.SetFloat(m_SpeedAnimatorParameter,value);
    }

    public void SetAimPose(bool b)
    {
        m_Animator.SetBool(m_AimWeaponParamter,b);
    }

    public void SetHasWeapon(bool b)
    {
        m_Animator.SetBool(m_HasWeaponParameter,b);
    }

    public void ShootWeapon(Action onShoot)
    {
        m_OnShootingPoseAction = onShoot;
        m_Animator.SetTrigger(m_ShootWeaponParameter);
    }

    #region Animator Events

    private void Shoot()
    {
        m_OnShootingPoseAction?.Invoke();
        m_OnShootingPoseAction = null;
    }
    
    #endregion
}
