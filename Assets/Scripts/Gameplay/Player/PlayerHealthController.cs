using System;
using UnityEngine;

public class PlayerHealthController : HealthController
{
    protected virtual void OnEnable()
    {
        base.OnEnable();
        GameEvents.GameplayEvents.RestoreHealth.Register(AddHealth);
        GameEvents.GameplayEvents.GainShield.Register(AddShield); ;
    }
    protected virtual void OnDisable()
    {
        base.OnDisable();
        GameEvents.GameplayEvents.RestoreHealth.Unregister(AddHealth);
        GameEvents.GameplayEvents.GainShield.Unregister(AddShield);
    }

    public void RegisterHealthBarEvent(Action<float> onHealthUpdateMethod)
    {
        m_HealthUpdate += onHealthUpdateMethod;
    }

    void AddHealth(float Health)
    {
        AddPlayerHealth(Health);
    }

    void AddShield(float Shield)
    {
        AddPlayerSheild(Shield);
    }
}
