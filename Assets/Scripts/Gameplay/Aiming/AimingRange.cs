using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimingRange : MonoBehaviour
{
    private Action<AimObject> m_OnAimObjectEnter;
    private Action<AimObject> m_OnAimObjectExit;

    public void Initialize(Action<AimObject> onEnter, Action<AimObject> onExit)
    {
        m_OnAimObjectEnter = onEnter;
        m_OnAimObjectExit = onExit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out AimObject aimObject))
        {
            if (!aimObject.IsAimable)
                return;
            
            m_OnAimObjectEnter?.Invoke(aimObject);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out AimObject aimObject))
        {
            m_OnAimObjectExit?.Invoke(aimObject);
        }
    }
}
