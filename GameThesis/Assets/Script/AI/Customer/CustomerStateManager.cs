using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public CustomerGiveMoneyBackState s_giveBackState = new CustomerGiveMoneyBackState();
    public CustomerRunEscapeState s_runEscapeState = new CustomerRunEscapeState();

    public CustomerDrunkState s_drunkState = new CustomerDrunkState();

    public CustomerFightState s_fightState = new CustomerFightState();
    public CustomerPushState s_pushState = new CustomerPushState();
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
    public Collider c_atkCollider;

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
    public GameObject text_coin;
    public Color color_escape;
    public float f_escapeTime;
    public bool b_escape;
    public float f_fightBackPercent;

    [Header("===== Pay =====")]
    public Color color_pay;
    public float f_payTime;
    [HideInInspector] public float f_giveCoin;
    public Vector2 v_minmaxGiveCoin;

    [Header("===== Run Escape =====")]
    public float f_runTime;
    public float f_runSpeed;
    public float f_walkSpeed;

    [Header("===== Dead State =====")]
    public float f_destroyTime;

    [Header("===== Drunk =====")]
    public Image img_wakeUpImage;
    public Image img_BGWakeUpImage;
    public bool b_isDrunk;
    public float f_drunkPercent;
    [HideInInspector] public float f_currentWekeUpPoint;
    public float f_wekeUpMultiply;
    public float f_maxWekeUpPoint;

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
        f_giveCoin = UnityEngine.Random.Range(v_minmaxGiveCoin.x, v_minmaxGiveCoin.y);
    }

    private void Update()
    {
        s_currentState.UpdateState(this);
        TextMeshProUGUI text = text_coin.GetComponent<TextMeshProUGUI>();
        text.text = $"$ {f_giveCoin.ToString("00.00")}";
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
        if (b_escape) GameManager.Instance.AddCoin(f_giveCoin);
        if (b_isDrunk) GameManager.Instance.AddCoin(f_giveCoin);

        RestaurantManager.Instance.RemoveRating();

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
        c_atkCollider.enabled = false;
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
            GameManager.Instance.AddCoin(f_giveCoin);
            SwitchState(s_goOutState);
        }
        else if (s_currentState == s_drunkState)
        {
            f_currentWekeUpPoint += f_wekeUpMultiply;
            if (f_currentWekeUpPoint >= f_maxWekeUpPoint)
            {
                SwitchState(s_giveBackState);
            }
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
        else if (s_currentState == s_frontCounter)
        {
            text = "[E] to Take Money";
        }
        else if (s_currentState == s_drunkState)
        {
            text = $"[E] to wake up the customer.{Environment.NewLine}" +
                $"Punch to drive away customers. ";
        }
        return text;
    }

    public void DestroyAI()
    {
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, f_atkRange);
    }
}
