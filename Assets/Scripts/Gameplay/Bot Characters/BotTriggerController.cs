using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotTriggerController : MonoBehaviour
{
    [SerializeField] private float m_ExtendedRange = 50f;
    [SerializeField] private SphereCollider m_LookTrigger;

    private Action<string, GameObject> m_OnTriggerEnter;
    private Action<string, GameObject> m_OnTriggerExit;

    private bool m_CanTrack = true;

    public void SeizeTracking()
    {
        m_CanTrack = false;
        enabled = false;
    }

    private void OnDisable()
    {
        m_OnTriggerEnter = null;
        m_OnTriggerExit = null;
    }

    public void ExtendLookRange()
    {
        if (m_LookTrigger.radius >= m_ExtendedRange)
            return;

        m_LookTrigger.radius = m_ExtendedRange;
    }

    public void Init(Action<string, GameObject> onTriggerEnter, Action<string, GameObject> onTriggerExit)
    {
        m_OnTriggerEnter = onTriggerEnter;
        m_OnTriggerExit = onTriggerExit;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!m_CanTrack)
            return;

        m_OnTriggerEnter?.Invoke(other.tag, other.gameObject);
    }

    private void OnTriggerExit(Collider other)
    {
        m_OnTriggerExit?.Invoke(other.tag, other.gameObject);
    }
}