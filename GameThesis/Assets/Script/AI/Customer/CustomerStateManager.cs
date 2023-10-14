using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class CustomerStateManager : StateManager, IDamageable, IInteracable
{
    public override BaseState s_currentState { get; set; }

    public CustomerWalkAroundState s_walkAroundState = new CustomerWalkAroundState();

    public CustomerGoToChairState s_goToChairState = new CustomerGoToChairState();
    public CustomerWaitFoodState s_waitFoodState = new CustomerWaitFoodState();
    public CustomerGoToCounterState s_goToCounterState = new CustomerGoToCounterState();
    public CustomerFrontOfCounterState s_frontCounter = new CustomerFrontOfCounterState();
    public CustomerEscapeState s_escapeState = new CustomerEscapeState();
    public CustomerGoOutFormRestaurantState s_goOutState = new CustomerGoOutFormRestaurantState();
    public CustomerEatFoodState s_eatFoodState = new CustomerEatFoodState();

    public CustomerFightState s_fightState = new CustomerFightState();
    public CustomerAttackState s_attackState = new CustomerAttackState();
    public CustomerDeadState s_deadState = new CustomerDeadState();
    public CustomerHurtState s_hurtState = new CustomerHurtState();

    [HideInInspector] public int i_currentHP;
    public int i_maxHP;

    [Header("===== Fight =====")]
    public float f_atkRange;
    public float f_atkDelay;
    [HideInInspector] public float f_currentAtkDelay;
    [HideInInspector] public bool b_canAtk;

    [Header("===== Attack =====")]
    public int i_atkCount;
    public Collider c_leftHandPunch;
    public Collider c_rightHandPunch;

    [Header("===== RagdollAndDrag =====")]
    public Transform t_hips;
    [HideInInspector] public Rigidbody[] rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;

    [Header("===== WalkAround =====")]
    public float f_findNextPositionTime;
    public Vector3 v_walkPos;

    [Header("===== Order Food =====")]
    public float f_orderTime;
    [HideInInspector] public float f_currentOrderTime;
    [HideInInspector] public TableObj c_tableObj;
    [HideInInspector] public ChairObj c_chairObj;

    [Header("===== Eat Food =====")]
    public Vector2 v_minAndMaxEatFood;
    public float f_randomEventPercent;

    [Header("===== Escape =====")]
    public Image img_progressBar;
    public Image img_icon;
    public Sprite sprite_escapeIcon;
    public float f_escapeTime;
    public bool b_escape;

    [Header("===== Dead State =====")]
    public float f_destroyTime;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentsInChildren<Rigidbody>();
    }

    private void Start()
    {
        s_currentState = s_walkAroundState;
        s_currentState.EnterState(this);
        i_currentHP = i_maxHP;
    }

    private void Update()
    {
        s_currentState.UpdateState(this);
    }

    public void TakeDamage(int damage)
    {
        i_currentHP -= damage;
        SwitchState(s_hurtState);

        if (i_currentHP <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        if(b_escape) GameManager.Instance.AddCoin(10f);

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

    public void Interaction()
    {
        if (s_currentState == s_deadState)
        {
            if (PlayerManager.Instance.g_dragObj == null)
            {
                PlayerManager.Instance.g_dragObj = this.gameObject;
                Rigidbody connectRb = PlayerManager.Instance.t_dragPos.GetComponent<Rigidbody>();

                SpringJoint spring = t_hips.AddComponent<SpringJoint>();
                spring.connectedBody = connectRb;
                spring.spring = 200f;
                spring.anchor = new Vector3(0, .85f, 0);
                spring.damper = .1f;
                spring.autoConfigureConnectedAnchor = false;
            }
        }
        else if (s_currentState == s_frontCounter)
        {
            GameManager.Instance.AddCoin(10f);
            SwitchState(s_goOutState);
        }

    }

    public string InteractionText()
    {
        string text = string.Empty;

        if (s_currentState == s_deadState)
        {
            if (PlayerManager.Instance.g_dragObj == null)
            {
                text = "[E] to Drag";
            }
        }
        else if(s_currentState == s_frontCounter)
        {
            text = "[E] to Take Money";
        }

        return text;
    }

    public void DestroyAI()
    {
        Destroy(gameObject);
    }

}
