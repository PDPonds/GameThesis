using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerStateManager : AIStateManager, IDamageable
{
    public override AIBaseState s_currentState { get; set; }

    public CustomerActivityState s_activityState = new CustomerActivityState();
    public CustomerFightState s_fightState = new CustomerFightState();
    public CustomerAttackState s_attackState = new CustomerAttackState();
    public CustomerDeadState s_deadState = new CustomerDeadState();
    public CustomerHurtState s_hurtState = new CustomerHurtState();

    public int i_HP;

    [Header("===== Fight =====")]
    public float f_atkRange;
    public float f_atkDelay;
    [HideInInspector] public float f_currentAtkDelay;
    [HideInInspector] public bool b_canAtk;

    [Header("===== Attack =====")]
    public int i_atkCount;
    public Collider c_leftHandPunch;
    public Collider c_rightHandPunch;

    [Header("===== Ragdoll =====")]
    [HideInInspector] public Rigidbody[] rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        s_currentState = s_activityState;
        s_currentState.EnterState(this);
    }

    private void Update()
    {
        s_currentState.UpdateState(this);
    }

    public void SwitchState(AIBaseState state)
    {
        s_currentState = state;
        state.EnterState(this);
    }

    public void TakeDamage(int damage)
    {
        i_HP -= damage;
        SwitchState(s_hurtState);

        if (i_HP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        SwitchState(s_deadState);
    }

    public void RagdollOn()
    {
        anim.enabled = false;
        agent.enabled = false;
        foreach (Rigidbody rb in rb) { rb.isKinematic = false; }
    }

    public void RagdollOff()
    {
        anim.enabled = true;
        agent.enabled = true;
        foreach (Rigidbody rb in rb) { rb.isKinematic = true; }
    }

    public void DisablePunch()
    {
        c_leftHandPunch.enabled = false;
        c_rightHandPunch.enabled = false;
    }

}
