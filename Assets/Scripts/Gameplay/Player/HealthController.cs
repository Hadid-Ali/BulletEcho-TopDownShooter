using System;
using UnityAtoms;
using UnityAtoms.BaseAtoms;
using UnityEngine;
using UnityEngine.UIElements;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float m_Health = 100;
    [SerializeField] private float m_Sheild = 0;
    [SerializeField] private FloatPairEvent m_HealthUpdateEvent;

    private float m_CurrentHealth, m_CurrentSheild;

    protected Action m_HealthDeminished;
    protected Action m_DamagedApplied;
    protected Action<float> m_HealthUpdate;

    public bool IsAlive => m_CurrentHealth > 0;


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
        Debug.Log($"Current Health {m_CurrentHealth}");

        Debug.Log($"Apply Damage On {gameObject} and Damage {damage}");

        if (m_CurrentSheild > 0)
        {
            m_CurrentSheild -= damage;
            GameEvents.GameplayUIEvents.UpdateShieldBar.Raise(m_CurrentSheild, m_Sheild);
        }
        else
        {
            m_CurrentHealth -= damage;
        }

        Debug.Log($"Apply Damage {m_CurrentHealth}");

        m_HealthUpdate?.Invoke(m_CurrentHealth / m_Health);
        m_DamagedApplied?.Invoke();
        m_HealthUpdateEvent?.Raise(new FloatPair()
        {
            Item1 = m_CurrentHealth,
            Item2 = m_Health
        });



        if (IsAlive)
            return;

        OnHealthDiminish();
    }

    public void AddPlayerHealth(float Health)
    {
        m_CurrentHealth += Health;

        m_HealthUpdate?.Invoke(m_CurrentHealth / m_Health);
        m_HealthUpdateEvent?.Raise(new FloatPair()
        {
            Item1 = m_CurrentHealth,
            Item2 = m_Health
        });
    }

    public void AddPlayerSheild(float shield)
    {
        m_Sheild += shield;
        m_CurrentSheild = m_Sheild;
        GameEvents.GameplayUIEvents.UpdateShieldBar.Raise(m_CurrentSheild, m_Sheild);
    }



    protected virtual void OnHealthDiminish()
    {
        m_HealthDeminished?.Invoke();
    }
}
