using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TimebasedAbility : AbilityObject
{
    [Header("Time Based Properties")]
    
    [SerializeField] protected float m_AbilityDuration = 5f;

    private WaitForEndOfFrame m_FrameWait = new();
    
    protected override void OnSpawn()
    {
        base.OnSpawn();
        StartCoroutine(AbilityRoutine());
    }

    private IEnumerator AbilityRoutine()
    {
        float time = m_AbilityDuration;

        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return m_FrameWait;
        }
        DestroyAbilityObject();
    }
}
