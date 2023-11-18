using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UIElements;

public enum EmployeeType { Cooking, Serve }

public class EmployeeStateManager : StateManager, IDamageable
{
    public override BaseState s_currentState { get; set; }
    public EmployeeType employeeType = EmployeeType.Cooking;

    [HideInInspector] public int i_currentHP;
    public int i_maxHP;
    public float f_walkSpeed;
    public float f_runSpeed;

    [Header("- Upgrade")]
    public float f_level2Mul;
    public float f_level3Mul;

    public EmployeeServeAndCookingState s_activityState = new EmployeeServeAndCookingState();
    public EmployeeSlackOffState s_slackOffState = new EmployeeSlackOffState();
    public EmployeePassedOutState s_passedOutState = new EmployeePassedOutState();
    public EmployeeHurtState s_hurtState = new EmployeeHurtState();

    [Header("===== Attack =====")]
    public int i_atkCount;

    [Header("===== Pressed Out =====")]
    public float f_pressedOutTime;
    public GameObject g_stunVFX;
    [HideInInspector] public Rigidbody[] rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;

    [Space(50)]
    [Header("===== Serve =====")]
    public bool b_canServe;
    public bool b_hasFood;
    [HideInInspector] public ChairObj s_serveChair;
    public GameObject g_Steak;
    public GameObject g_Staw;
    public GameObject g_Bacon;


    [Header("===== Cooking =====")]
    public bool b_canCook;
    public bool b_isWorking;
    public Transform t_workingPos;
    public float f_cookingTime;

    [HideInInspector] public ChairObj s_cookingChair;
    public AnimatorOverrideController cookingAnim;

    [Header("===== Slack Off =====")]
    public bool b_onSlackOffPoint;
    public float f_timeToSlackOff;
    public float f_slackOffPercent;
    public Vector2 v_minmaxX;
    public Vector2 v_minmaxZ;
    public Vector2 v_minAndMaxSlackOffTime;
    public List<AnimatorOverrideController> allSlackOffAnim = new List<AnimatorOverrideController>();
    [HideInInspector] public Vector3 v_walkPos;
    [HideInInspector] public float f_slackOffTime;

    [Header("===== Area =====")]
    public AreaType currentAreaStay;

    [Header("===== Outline =====")]
    public Transform t_mesh;
    public List<Transform> meshs = new List<Transform>();
    public float f_outlineScale;
    public Color color_warning;
    public Color color_fighting;

    MaterialPropertyBlock mpb;
    public MaterialPropertyBlock Mpb
    {
        get
        {
            if (mpb == null)
            {
                mpb = new MaterialPropertyBlock();
            }
            return mpb;
        }
    }

    public void ApplyOutlineColor(Color color, float scale)
    {
        Mpb.SetColor("_Color", color);
        Mpb.SetFloat("_Scale", scale);
        foreach (Transform t in meshs)
        {
            SkinnedMeshRenderer trnd = t.GetComponent<SkinnedMeshRenderer>();
            trnd.SetPropertyBlock(mpb);
        }

    }

    private void Awake()
    {

        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentsInChildren<Rigidbody>();

        if (employeeType == EmployeeType.Serve) b_canServe = true;

        Color noColor = new Color(0, 0, 0, 0);
        ApplyOutlineColor(noColor, 0f);
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


}
