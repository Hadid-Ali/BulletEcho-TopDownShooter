using System;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float m_Health = 100;
    [SerializeField] private FloatPairEvent m_HealthUpdateEvent;
    
    private float m_CurrentHealth;
    
    protected Action m_HealthDeminished;
    protected Action m_DamagedApplied;
    protected Action<float> m_HealthUpdate;

    protected virtual void OnEnable()
    {
        m_CurrentHealth = m_Health;
    }

    public void Initialize(Action OnHealthDiminished, Action<float> OnHealthUpdate = null, Action OnDamage = null)
    {
        m_HealthUpdate += OnHealthUpdate;
        m_HealthDeminished = OnHealthDiminished;
        m_DamagedApplied = OnDamage;
    }

    protected virtual void OnDisable()
    {
        m_HealthDeminished = null;
        m_HealthUpdate = null;
        m_DamagedApplied = null;
    }

    public virtual void ApplyDamage(float damage)
    {
        Debug.LogError($"Apply Damage On {gameObject}");
        m_CurrentHealth -= damage;

        m_HealthUpdate?.Invoke(m_CurrentHealth / m_Health);
        m_DamagedApplied?.Invoke();
        m_HealthUpdateEvent?.Raise(new FloatPair()
        {
             Item1 = m_CurrentHealth,
             Item2 = m_Health
        });
        if (m_CurrentHealth > 0)
            return;

        OnHealthDiminish();
    }

    protected virtual void OnHealthDiminish()
    {
        m_HealthDeminished?.Invoke();
    }
}
