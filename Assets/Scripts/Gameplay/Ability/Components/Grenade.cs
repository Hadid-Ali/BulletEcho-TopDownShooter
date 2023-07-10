using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : InstantAbility
{
    [Header("Grenade Properties")]
    [Header("Component Refs")] 
    
    [SerializeField] private ParticleSystem m_BlastParticle;
    [SerializeField] private Collider m_DamageCollider;

    [Header("Properties")]
    
    [SerializeField] private float m_Damage = 25f;
    [SerializeField] private float m_BlastRadius = 3f;
    [SerializeField] private float m_SpawnOffset = 1f;

    [SerializeField] private LayerMask m_DamageLayer;
    private Transform m_Caster;

    public override void Execute(Transform abilityPlayerTransform)
    {
        m_Caster = abilityPlayerTransform;
        Transform.position = abilityPlayerTransform.position + abilityPlayerTransform.forward * m_SpawnOffset;

        m_BlastParticle.Play(true);
        Invoke(nameof(ApplyDamage), 0.15f);
    }

    private void ApplyDamage()
    {
        m_DamageCollider.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out HealthController healthController) || healthController == null ||
            healthController.gameObject == m_Caster.gameObject)
            return;

        healthController.ApplyDamage(m_Damage);
    }
}