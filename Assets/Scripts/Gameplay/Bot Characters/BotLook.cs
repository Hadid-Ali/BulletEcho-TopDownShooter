using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BotLook : MonoBehaviour
{
    [SerializeField] private Transform m_LookPoint;
    [SerializeField] private LayerMask m_VisionMask;
    
    [SerializeField] private float m_VisionRange;
    [SerializeField] private Vector3 m_VisionOffset = new Vector3(0f, 0.5f, 0f);
    
    public GameObject ObjectUnderView(Transform focusObject)
    {
        Vector3 rayDirection = ((focusObject.position + m_VisionOffset) - m_LookPoint.position).normalized;
        
        if (Physics.Raycast(m_LookPoint.position, rayDirection,
                out RaycastHit underViewObject, m_VisionRange, m_VisionMask))
        {
            return underViewObject.transform.gameObject;
        }
        return null;
    }

    public bool CheckUnderView(Transform focusObject)
    {
        Vector3 rayDirection = ((focusObject.position + m_VisionOffset) - m_LookPoint.position).normalized;

        return Physics.Raycast(m_LookPoint.position, rayDirection,
            out RaycastHit underViewObject, m_VisionRange, m_VisionMask);
    }
}
