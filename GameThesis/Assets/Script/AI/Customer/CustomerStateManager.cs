using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Burst.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

[Serializable]
public class CustomerClothes
{
    public int hair;
    public int shirt;
    public int pant;
    public int hat;
}

public class CustomerStateManager : StateManager, IDamageable, IInteracable
{
    public CustomerClothes CustomerClothes;

    public List<GameObject> hairs = new List<GameObject>();
    public List<GameObject> shirts = new List<GameObject>();
    public List<GameObject> pants = new List<GameObject>();
    public List<GameObject> hats = new List<GameObject>();

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

    public CustomerCrowdState s_crowdState = new CustomerCrowdState();

    public CustomerFightState s_fightState = new CustomerFightState();
    public CustomerPushState s_pushState = new CustomerPushState();
    public CustomerDeadState s_deadState = new CustomerDeadState();
    public CustomerHurtState s_hurtState = new CustomerHurtState();

    [HideInInspector] public int i_currentHP;
    public int i_maxHP;

    [Header("===== Fight =====")]
    [HideInInspector] public bool b_inFight;
    [HideInInspector] public bool b_fightWithPlayer;
    public Collider c_atkCol;
    public float f_waitDis;
    public float f_fightDis;

    [Header("- Fight")]
    public float f_atkDelay;
    public float f_attackRange;

    [Header("- Player Escape")]
    public float f_playerEscapeTime;

    [HideInInspector] public int i_atkCount;
    [Space(10f)]

    [Header("===== RagdollAndDrag =====")]
    public Transform t_hips;
    [HideInInspector] public Rigidbody[] rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;
    [Space(10f)]

    [Header("===== WalkAround =====")]
    public float f_findNextPositionTime;
    public Vector3 v_walkPos;
    [Space(10f)]

    [Header("===== Order Food =====")]
    public float f_orderTime;
    [HideInInspector] public float f_currentOrderTime;
    [HideInInspector] public ChairObj c_chairObj;
    [Space(10f)]

    [Header("===== Eat Food =====")]
    public Vector2 v_minAndMaxEatFood;
    public float f_randomEventPercent;
    [Space(10f)]

    [Header("===== Escape =====")]
    public bool b_escape;
    public float f_fightBackPercent;
    [Space(10f)]

    [Header("===== Pay =====")]
    public float f_payTime;
    [HideInInspector] public float f_giveCoin;
    public Vector2 v_minmaxGiveCoin;
    [Space(10f)]

    [Header("===== Run Escape =====")]
    public float f_runTime;
    public float f_runSpeed;
    public float f_walkSpeed;
    [Space(10f)]

    [Header("===== Dead State =====")]
    public float f_destroyTime;
    public GameObject g_stunVFX;
    [Space(10f)]

    [Header("===== Drunk =====")]
    public bool b_isDrunk;
    public float f_drunkPercent;
    [HideInInspector] public float f_currentWekeUpPoint;
    public float f_wekeUpMultiply;
    public float f_maxWekeUpPoint;
    public GameObject g_sleepVFX;
    [Space(10f)]

    [Header("===== Gangster =====")]
    public float f_isGangsterPercent;
    public bool b_hasGang;
    public Vector2Int v_minmaxGangCount;
    [HideInInspector] public bool b_isGang;
    [HideInInspector] public int i_gangCount;
    [HideInInspector] public int i_prefabIndex;
    [HideInInspector] public int i_spawnPosIndex;
    [Space(10f)]

    [Header("===== Area =====")]
    public AreaType currentAreaStay;
    [Space(10f)]

    [Header("===== Outline =====")]
    public Transform t_mesh;
    public Color color_warning;
    public Color color_interact;
    public Color color_inFighting;
    public Color color_fightWithPlayer;
    public float f_outlineScale;

    SkinnedMeshRenderer meshrnd;
    SkinnedMeshRenderer hairrnd;
    SkinnedMeshRenderer shirtrnd;
    SkinnedMeshRenderer pantrnd;
    SkinnedMeshRenderer hatrnd;

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
    [Space(10f)]

    [Header("===== Cheer =====")]
    public List<AnimatorOverrideController> allCheerAnim = new List<AnimatorOverrideController>();
    [HideInInspector] public Vector3 v_crowdPos;

    public void ApplyOutlineColor(Color color, float scale)
    {
        meshrnd = t_mesh.GetComponent<SkinnedMeshRenderer>();

        if (CustomerClothes.hair >= 0) hairrnd = hairs[CustomerClothes.hair].GetComponent<SkinnedMeshRenderer>();
        shirtrnd = shirts[CustomerClothes.shirt].GetComponent<SkinnedMeshRenderer>();
        pantrnd = pants[CustomerClothes.pant].GetComponent<SkinnedMeshRenderer>();
        if (CustomerClothes.hat >= 0) hatrnd = hats[CustomerClothes.hat].GetComponent<SkinnedMeshRenderer>();

        Mpb.SetColor("_Color", color);
        Mpb.SetFloat("_Scale", scale);

        meshrnd.SetPropertyBlock(mpb);
        if (hairrnd != null) hairrnd.SetPropertyBlock(mpb);
        if (shirtrnd != null) shirtrnd.SetPropertyBlock(mpb);
        if (pantrnd != null) pantrnd.SetPropertyBlock(mpb);
        if (hatrnd != null) hatrnd.SetPropertyBlock(mpb);
    }

    public CustomerClothes GenerateClothes()
    {
        CustomerClothes clothes = new CustomerClothes();
        int hair = UnityEngine.Random.Range(-1, hairs.Count);
        int shirt = UnityEngine.Random.Range(0, shirts.Count);
        int pant = UnityEngine.Random.Range(0, pants.Count); ;
        int hat = UnityEngine.Random.Range(-1, hats.Count);
        clothes.hair = hair;
        clothes.shirt = shirt;
        clothes.pant = pant;
        clothes.hat = hat;
        return clothes;
    }

    public CustomerClothes SetUpClothes(int hair, int shirt, int pant, int hat)
    {
        CustomerClothes clothes = new CustomerClothes();
        clothes.hair = hair;
        clothes.shirt = shirt;
        clothes.pant = pant;
        clothes.hat = hat;
        return clothes;
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        rb = GetComponentsInChildren<Rigidbody>();

        c_atkCol.enabled = false;

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

        for (int i = 0; i < hairs.Count; i++)
        {
            if (i != CustomerClothes.hair) hairs[i].SetActive(false);
            else hairs[i].SetActive(true);
        }

        for (int i = 0; i < shirts.Count; i++)
        {
            if (i != CustomerClothes.shirt) shirts[i].SetActive(false);
            else shirts[i].SetActive(true);
        }

        for (int i = 0; i < pants.Count; i++)
        {
            if (i != CustomerClothes.pant) pants[i].SetActive(false);
            else pants[i].SetActive(true);
        }

        for (int i = 0; i < hats.Count; i++)
        {
            if (i != CustomerClothes.pant) hats[i].SetActive(false);
            else hats[i].SetActive(true);
        }

    }

    public void TakeDamage(int damage)
    {
        i_currentHP -= damage;

        s_hurtState.s_lastState = s_currentState;

        if (i_currentHP <= 0) Die();
        else SwitchState(s_hurtState);

    }

    public void Die()
    {
        b_inFight = false;
        b_isGang = false;

        if (b_escape) GameManager.Instance.AddCoin(f_giveCoin);
        if (b_isDrunk) GameManager.Instance.AddCoin(f_giveCoin);

        if (b_hasGang && !b_isGang)
        {
            i_gangCount = UnityEngine.Random.Range(v_minmaxGangCount.x, v_minmaxGangCount.y);
            for (int i = 0; i < i_gangCount; i++)
            {
                GameManager.Instance.s_gameState.SpawnCustomerGang(i_prefabIndex, CustomerClothes);
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
                c_chairObj.b_isEmpty = true;
                c_chairObj.b_readyForNextCustomer = false;
                c_chairObj.s_currentCustomer = null;

                c_chairObj = null;
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
            text = $"[E] to wake up the customer.";
        }
        return text;
    }

    public void DestroyAI()
    {
        Destroy(gameObject);
    }

}
