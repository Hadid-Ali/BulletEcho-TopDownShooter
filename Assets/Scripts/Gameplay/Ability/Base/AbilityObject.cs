using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbilityObject : MonoBehaviour
{
    [Header("Default Properties")]
    
    [SerializeField] private float m_TrivialTime = 2f;

    private Transform m_Transform;
    
    protected Transform Transform
    {
        get
        {
            if (m_Transform == null)
                m_Transform = transform;

            return m_Transform;
        }
    }

    private void Awake()
    {
        m_Transform = transform;
    }

    private void OnEnable()
    {
        OnSpawn();
    }

    private void OnDestroy()
    {
        Resources.UnloadUnusedAssets();
    }

    public abstract void Execute(Transform transform);
    
    protected virtual void OnSpawn()
    {
    }

    protected virtual void DestroyAbilityObject()
    {
        Destroy(gameObject, m_TrivialTime);
    }
}
