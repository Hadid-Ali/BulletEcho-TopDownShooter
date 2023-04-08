using System;
using UnityEngine;
using System.Collections.Generic;

public class WeaponAimingComponent : MonoBehaviour
{
    [SerializeField] private PlayerController m_PlayerController;
    [SerializeField] private AimingRange m_AimingRange;

    private List<AimObject> m_AimObjects = new();

    private AimObject m_CurrentAimObject;
    private bool m_CanAim = true;

    private event Action<AimObject> m_TargetsStatusUpdated;

    public AimObject CurrentAimObject => m_AimObjects[0];
    
    private void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
    }

    private void OnEnable()
    {
        m_AimingRange.Initialize(AddAimObject, RemoveAimObject);
    }

    public void Initialize(Action<AimObject> OnTargetsStatusUpdated)
    {
        m_TargetsStatusUpdated += OnTargetsStatusUpdated;
    }

    private void AddAimObject(AimObject aimObject)
    {
        if (m_AimObjects.Contains(aimObject))
            return;
        
        m_AimObjects.Add(aimObject);
        SetAimTarget();
    }

    private void RemoveAimObject(AimObject aimObject)
    {
        m_AimObjects.Remove(aimObject);
        SetAimTarget();
    }

    private void SetAimTarget()
    {
        m_TargetsStatusUpdated?.Invoke(m_AimObjects.Count > 0 ? m_AimObjects[0] : null);
    }
}
