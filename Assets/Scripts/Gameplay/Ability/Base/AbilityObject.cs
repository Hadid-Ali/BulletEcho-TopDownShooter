using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityObject : MonoBehaviour
{
    [Header("Default Properties")]
    
    [SerializeField] private float m_TrivialTime = 2f;

    private void OnEnable()
    {
        Execute();
        OnSpawn();
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }
    
    protected abstract void Execute();
    
    protected virtual void OnSpawn()
    {
        DestroyAbilityObject();
    }

    protected virtual void DestroyAbilityObject()
    {
        Destroy(gameObject, m_TrivialTime);
    }
}
