using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerRangeHandler : MonoBehaviour
{
    [SerializeField] private PlayerRange m_PlayerRange;

    private void OnEnable()
    {
        m_PlayerRange.Initialize(OnObjectEnterRange, OnObjectLeaveRange);
    }

    protected abstract void OnObjectEnterRange(GameObject rangeObject);
    protected abstract void OnObjectLeaveRange(GameObject rangeObject);
}
