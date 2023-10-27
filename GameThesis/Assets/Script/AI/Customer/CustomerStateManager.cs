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

    public CustomerAggressiveChaseState s_aggressive = new CustomerAggressiveChaseState();

    public CustomerCrowdState s_crowdState = new CustomerCrowdState();

    public CustomerFightState s_fightState = new CustomerFightState();
    public CustomerPushState s_pushState = new CustomerPushState();
    public CustomerAttackState s_attackState = new CustomerAttackState();
    public CustomerDeadState s_deadState = new CustomerDeadState();
    public CustomerHurtState s_hurtState = new CustomerHurtState();

    [HideInInspector] public int i_currentHP;
    public int i_maxHP;

    [Header("===== Fight =====")]
    public float f_atkRange;
    public float f_runRange;

    public float f_atkDelay;
    public float f_fightTime;
    [HideInInspector] public float f_currentFightTime;

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
    public TableObj c_tableObj;
    public ChairObj c_chairObj;

    [Header("===== Eat Food =====")]
    public Vector2 v_minAndMaxEatFood;
    public float f_randomEventPercent;

    [Header("===== Escape =====")]
    public bool b_escape;
    public float f_fightBackPercent;

    [Header("===== Pay =====")]
    public float f_payTime;
    [HideInInspector] public float f_giveCoin;
    public Vector2 v_minmaxGiveCoin;

    [Header("===== Run Escape =====")]
    public float f_runTime;
    public float f_runSpeed;
    public float f_walkSpeed;

    [Header("===== Dead State =====")]
    public float f_destroyTime;
    public GameObject g_stunVFX;

    [Header("===== Drunk =====")]
    public bool b_isDrunk;
    public float f_drunkPercent;
    [HideInInspector] public float f_currentWekeUpPoint;
    public float f_wekeUpMultiply;
    public float f_maxWekeUpPoint;
    public GameObject g_sleepVFX;

    [Header("===== Gangster =====")]
    public float f_isGangsterPercent;
    public bool b_hasGang;
    public Vector2Int v_minmaxGangCount;
    [HideInInspector] public bool b_isGang;
    [HideInInspector] public int i_gangCount;
    [HideInInspector] public int i_prefabIndex;
    [HideInInspector] public int i_spawnPosIndex;

    [Header("===== Area =====")]
    public AreaType currentAreaStay;

    [Header("===== Outline =====")]
    public Transform t_mesh;
    public Color color_warning;
    public Color color_interact;
    public Color color_fighting;
    public float f_outlineScale;

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

    [Header("===== Throng =====")]
    [HideInInspector] public Vector3 v_crowdPos;

    public void ApplyOutlineColor(Color color, float scale)
    {
        SkinnedMeshRenderer rnd = t_mesh.GetComponent<SkinnedMeshRenderer>();
        Mpb.SetColor("_Color", color);
        Mpb.SetFloat("_Scale", scale);
        rnd.SetPropertyBlock(mpb);
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentsInChildren<Rigidbody>();

        Color noColor = new Color(0, 0, 0, 0);
        ApplyOutlineColor(noColor, 0f);
    }

    private void Start()
    {
        if (!b_isGang)
        {
            s_currentState = s_walkAroundState;
            s_currentState.EnterState(this);
        }

        i_currentHP = i_maxHP;
        f_giveCoin = UnityEngine.Random.Range(v_minmaxGiveCoin.x, v_minmaxGiveCoin.y);

    }

    private void Update()
    {
        s_currentState.UpdateState(this);

        if (s_currentState == s_fightState || s_currentState == s_attackState)
        {
            if (PlayerManager.Instance.b_inFighting)
            {
                f_currentFightTime = f_fightTime;
            }
            else
            {
                f_currentFightTime -= Time.deltaTime;
                if (f_currentFightTime < 0)
                {
                    SwitchState(s_walkAroundState);
                }
            }
        }

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

        if (b_hasGang)
        {
            i_gangCount = UnityEngine.Random.Range(v_minmaxGangCount.x, v_minmaxGangCount.y);
            for (int i = 0; i < i_gangCount; i++)
            {
                GameManager.Instance.s_gameState.SpawnCustomerGang(i_prefabIndex);
            }
        }

        if (currentAreaStay == AreaType.InRestaurant)
        {
            RestaurantManager.Instance.RemoveRating();
        }

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
                c_tableObj.b_isEmpty = true;
                c_tableObj.b_readyForNextCustomer = false;
                c_tableObj.s_currentCustomer = null;

                c_tableObj = null;
                c_chairObj = null;

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
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, f_atkRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, f_runRange);

    }

}
