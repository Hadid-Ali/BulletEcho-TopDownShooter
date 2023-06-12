using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRange : MonoBehaviour
{
    private Action<GameObject> m_OnObjectEnterRange;
    private Action<GameObject> m_OnObjectExit;

    public void Initialize(Action<GameObject> onEnter, Action<GameObject> onExit)
    {
        m_OnObjectEnterRange += onEnter;
        m_OnObjectExit += onExit;
    }

    private void OnTriggerEnter(Collider other)
    {
        m_OnObjectEnterRange?.Invoke(other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        m_OnObjectExit?.Invoke(other.gameObject);
    }
}
