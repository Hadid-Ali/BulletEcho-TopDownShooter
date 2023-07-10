using System;
using UnityEngine;

public enum AgentState
{
    Idle,
    Patrolling,
    Suspicious,
    Chasing,
    Attack,
    Dead
}

public class NavigationAgent : MonoBehaviour
{
    [SerializeField] private NavigationAgentMovement m_NavigationAgentMovement;
    [SerializeField] protected BotAnimatorController m_AnimatorController;
    [SerializeField] protected BotLook m_BotLook;
    [SerializeField] private BotTriggerController m_BotTriggerController;
    [SerializeField] private Transform m_Transform;
    [SerializeField] private HealthController m_HealthController;
    
    protected Transform m_Target;
    protected PlayerController m_PlayerController;

    private AgentState m_State;
    private AgentState m_PreviousState;

    private Action<AgentState> m_AgentStateChange;

    private void Start()
    {
        Init();
        
        m_AgentStateChange += OnStateChange;
    }

    protected virtual void Init()
    {
        m_NavigationAgentMovement ??= GetComponent<NavigationAgentMovement>();
        m_AnimatorController ??= GetComponent<BotAnimatorController>();
        m_BotLook ??= GetComponent<BotLook>();
        m_BotTriggerController ??= GetComponent<BotTriggerController>();
        m_HealthController ??= GetComponent<HealthController>();

        m_BotTriggerController.Init(OnObjectEnterRange, OnObjectExitRange);
        m_Transform ??= transform;

        if (m_HealthController is null)
            return;

        m_HealthController.Initialize(Die, null, GetDamage);
    }

    protected virtual void GetDamage()
    {
        m_AnimatorController.SetDamagePose();
        m_BotTriggerController.ExtendLookRange();
    }

    protected virtual void Die()
    {
        ChangeState(AgentState.Dead);
    }
    
    private void Update()
    {
        StatesEngine();
    }

    protected void LookTowards(Transform target)
    {
        Vector3 v = m_Transform.eulerAngles;
        m_Transform.LookAt(target);
        v.y = m_Transform.eulerAngles.y;

        m_Transform.eulerAngles = v;
    }
    
    private void StatesEngine()
    {
        switch (m_State)
        {
            case AgentState.Suspicious:
                SuspiciousState();
                break;
            
            case AgentState.Attack:
                AttackState();
                break;
        }
    }

    public virtual void SuspiciousState()
    {
        CheckCurrentTarget();
    }

    public virtual void AttackState()
    {
        LookTowards(m_Target);
    }

    private void ChangeState(AgentState agentState)
    {
        if(m_State is AgentState.Dead)
            return;
        
        m_PreviousState = m_State;
        m_State = agentState;
        m_AgentStateChange?.Invoke(agentState);
    }

    private void OnStateChange(AgentState state)
    {
        switch (state)
        {
            case AgentState.Chasing:
                OnSwitchToChase();
                break;
            
            case AgentState.Attack:
                OnSwitchToAttack();
                break;
            
            case AgentState.Dead:
                OnSwitchToDie();
                break;
        }
    }

    protected virtual void OnSwitchToDie()
    {
        m_AnimatorController.DiePose();
        m_NavigationAgentMovement.StopMovement(true);
        m_BotTriggerController.SeizeTracking();
    }

    protected virtual void OnSwitchToChase()
    {
        m_NavigationAgentMovement.SetNavigationTowards(m_PlayerController.transform, MovementMode.Run,
            OnAttackDistanceReach,
            OnStartChase);
    }

    protected virtual void OnSwitchToAttack()
    {
        
    }

    private void OnAttackDistanceReach()
    {
        m_AnimatorController.SetIdle();
        ChangeState(AgentState.Attack);
    }

    private void OnStartChase()
    {
        m_AnimatorController.SetSpeed(5f);
        ChangeState(AgentState.Chasing);
    }

    [ContextMenu("Suspecious State")]
    private void SuspeciousState()
    {
        ChangeState(AgentState.Suspicious);
    }
    
    private void CheckCurrentTarget()
    {
        GameObject underViewObject = m_BotLook.ObjectUnderView(m_Target);
        if (underViewObject.TryGetComponent(out m_PlayerController))
        {
            ChangeState(AgentState.Chasing);
        }
    }

    protected virtual void OnObjectEnterRange(string trigger, GameObject enteredObject)
    {
        if (m_State is not (AgentState.Idle or AgentState.Patrolling))
            return;

        if (trigger == "Player")
        {
            m_Target = enteredObject.transform;
            ChangeState(AgentState.Suspicious);
        }
    }

    protected virtual  void OnObjectExitRange(string trigger, GameObject enteredObject)
    {
        if(m_State is not (AgentState.Attack or AgentState.Chasing or AgentState.Dead))
            return;

        if (m_Target == enteredObject.transform)
        {
            m_Target = null;
            ChangeState(m_PreviousState);
        }
    }
}
