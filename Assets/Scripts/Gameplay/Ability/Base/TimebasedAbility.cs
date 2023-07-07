using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimebasedAbility : AbilityObject
{
    [SerializeField] protected float m_AbilityDuration = 5f;

    public override void OnSpawn()
    {
        base.OnSpawn();
        Invoke(nameof(DestroyAbilityObject), m_AbilityDuration);
    }
}
