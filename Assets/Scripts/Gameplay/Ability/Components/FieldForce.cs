using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldForce : TimebasedAbility
{
    private HealthController m_HealthController;
    
    public override void Execute(Transform abilityPlayerTransform)
    {
        Transform.SetParent(abilityPlayerTransform);
        Transform.localPosition = Vector3.zero;

        if (abilityPlayerTransform.TryGetComponent(out m_HealthController))
        {
            m_HealthController.SetHealthStatus(HealthStatus.UnderProtection);
        }
    }

    protected override void DestroyAbilityObject()
    {
        m_HealthController.RestoreHealthStatus();
        base.DestroyAbilityObject();
    }
}
