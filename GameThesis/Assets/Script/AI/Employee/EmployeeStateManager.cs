using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum EmployeeType { Cooking, Serve }

public class EmployeeStateManager : StateManager, IDamageable
{
    public override BaseState s_currentState { get; set; }
    public EmployeeType employeeType = EmployeeType.Cooking;

    [HideInInspector] public int i_currentHP;
    public int i_maxHP;
    public float f_walkSpeed;
    public float f_runSpeed;

    public EmployeeServeAndCookingState s_activityState = new EmployeeServeAndCookingState();

    public EmployeeSlowDownState s_slowDownState = new EmployeeSlowDownState();

    public EmployeePassedOutState s_passedOutState = new EmployeePassedOutState();

    public EmployeeHurtState s_hurtState = new EmployeeHurtState();
    public EmployeeFightState s_fightState = new EmployeeFightState();
    public EmployeeAttackState s_attackState = new EmployeeAttackState();

    [Header("===== Fight =====")]
    public float f_atkRange;
    public float f_atkDelay;
    [HideInInspector] public float f_currentAtkDelay;
    [HideInInspector] public bool b_canAtk;

    [Header("===== Attack =====")]
    public int i_atkCount;
    public Collider c_leftHandPunch;
    public Collider c_rightHandPunch;

    [Header("===== Pressed Out =====")]
    public float f_pressedOutTime;

    [HideInInspector] public Rigidbody[] rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;

    [Space(50)]
    [Header("===== Serve =====")]
    public bool b_canServe;
    public bool b_hasFood;
    public TableObj s_serveTable;

    [Header("===== Cooking =====")]
    public bool b_isWorking;
    public Transform t_workingPos;

    [Header("===== Slow Down =====")]
    public float f_timeToSlow;
    public float f_slowPercent;
    public Vector2 v_minmaxX;
    public Vector2 v_minmaxZ;
    public Vector2 v_minAndMaxSlowTime;
    [HideInInspector] public Vector3 v_walkPos;
    [HideInInspector] public float f_slowTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentsInChildren<Rigidbody>();

        if (employeeType == EmployeeType.Serve) b_canServe = true;

    }

    void Start()
    {
        i_currentHP = i_maxHP;
        SwitchState(s_activityState);
    }

    void Update()
    {
        s_currentState.UpdateState(this);
    }

    public void TakeDamage(int damage)
    {
        i_currentHP -= damage;

        s_hurtState.s_lastState = s_currentState;

        SwitchState(s_hurtState);

        if (i_currentHP <= 0)
        {
            Die();
        }

    }

    public void Die()
    {
        SwitchState(s_passedOutState);
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
