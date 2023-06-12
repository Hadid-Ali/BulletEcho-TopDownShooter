using System;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Serialization;

public class WeaponAimingComponent : PlayerRangeHandler
{
    [SerializeField] private PlayerController m_PlayerController;
    private List<AimObject> m_AimObjects = new();

    private AimObject m_CurrentAimObject;
    private bool m_CanAim = true;

    private event Action<AimObject> m_TargetsStatusUpdated;

    public AimObject CurrentAimObject => m_AimObjects[0];
    
    private void Start()
    {
        m_PlayerController = GetComponent<PlayerController>();
    }
    
    public void Initialize(Action<AimObject> OnTargetsStatusUpdated)
    {
        m_TargetsStatusUpdated += OnTargetsStatusUpdated;
    }
    
    private void SetAimTarget()
    {
        m_TargetsStatusUpdated?.Invoke(m_AimObjects.Count > 0 ? m_AimObjects[0] : null);
    }

    protected override void OnObjectEnterRange(GameObject rangeObject)
    {
        if (!rangeObject.TryGetComponent(out AimObject aimObject) || !aimObject.IsAimable ||
            m_AimObjects.Contains(aimObject))
            return;

        m_AimObjects.Add(aimObject);
        SetAimTarget();
    }

    protected override void OnObjectLeaveRange(GameObject rangeObject)
    {
        if (!rangeObject.TryGetComponent(out AimObject aimObject)) 
            return;
        
        m_AimObjects.Remove(aimObject);
        SetAimTarget();
    }
}
