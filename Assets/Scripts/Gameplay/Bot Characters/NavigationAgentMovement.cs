using System;
using UnityEngine;
using UnityEngine.AI;

public enum MovementMode
{
    Walk,
    Run
}

public class NavigationAgentMovement : MonoBehaviour
{
    [SerializeField] private float m_WalkingSpeed;
    [SerializeField] private float m_RunningSpeed;
    
    [SerializeField] private float m_StoppingDistance;
    [SerializeField] private float m_StoppingDistanceOffset = 2f;

    private MovementMode m_NavigationMode;
    private NavMeshAgent m_NavmeshAgent;

    private Transform m_Transform;
    private Transform m_TargetToFollow;

    private Action m_OnTargetReached;
    private Action m_OnTargetChaseStart;
    
    private bool m_IsMoving = false;
    private float m_MovementSpeed;

    private float DistanceFromTarget => m_TargetToFollow is null
        ? Mathf.Infinity
        : Vector3.Distance(m_TargetToFollow.position, m_Transform.position);
    private bool IsTargetNearby => DistanceFromTarget <= m_StoppingDistance;
    
    private void Start()
    {
        m_Transform = transform;
        m_NavmeshAgent ??= GetComponent<NavMeshAgent>();
        m_NavmeshAgent.stoppingDistance = m_StoppingDistance;
    }

    public void SetNavigationTowards(Transform target, MovementMode movementMode,Action onTargetReached,Action onTargetChaseStart)
    {
        m_TargetToFollow = target;

        m_OnTargetChaseStart = onTargetChaseStart;
        m_OnTargetReached = onTargetReached;

        SetMovementMode(movementMode);
        StartMovement();
    }

    private void SetMovementMode(MovementMode movementMode)
    {
        m_NavigationMode = movementMode;
        SetMovementSpeed(movementMode is MovementMode.Walk ? m_WalkingSpeed : m_RunningSpeed);
    }

    private void MoveToTarget()
    {
        if(!m_IsMoving)
            return;
        
        m_NavmeshAgent.SetDestination(m_TargetToFollow.position);
    }

    private void CheckDistanceFromTarget()
    {
        if (m_IsMoving)
        {
            if (!IsTargetNearby)
                return;

            StopMovement(false);
        }
        else
        {
            if (DistanceFromTarget > m_StoppingDistance + m_StoppingDistanceOffset)
            {
                StartMovement();
            }
        }
    }

    private void StartMovement()
    {
        if (m_IsMoving)
            return;

        m_IsMoving = true;
        SetMovingStatus(true);
        m_OnTargetChaseStart?.Invoke();
    }

    public void StopMovement(bool  seizeMovement)
    {
        SetMovingStatus(false);
        m_OnTargetReached?.Invoke();
        m_IsMoving = false;
        
        if (seizeMovement)
        {
            m_TargetToFollow = null;
        }
    }

    private void SetMovingStatus(bool isMoving)
    {
        m_IsMoving = isMoving;
        m_NavmeshAgent.isStopped = !isMoving;
    }
    
    private void SetMovementSpeed(float speed)
    {
        m_NavmeshAgent.speed = m_MovementSpeed = speed;
    }

    private void Update()
    {
        if(m_TargetToFollow is null)
            return;

        CheckDistanceFromTarget();
        MoveToTarget();
    }
}