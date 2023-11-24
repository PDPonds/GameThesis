using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerManager : Auto_Singleton<PlayerManager>
{
    public int i_maxHP;
    [HideInInspector] public int i_currentHP;
    [HideInInspector] public Rigidbody c_rb;
    [HideInInspector] public CapsuleCollider c_collider;
    [HideInInspector] public InputSystem s_playerInput;
    [HideInInspector] public PlayerMovement s_playerMovement;
    [HideInInspector] public FistCombat s_playerFistCombat;
    [HideInInspector] public PlayerGuard s_playerGuard;
    [HideInInspector] public PlayerSprint s_playerSprint;
    [Space(10f)]

    [Header("===== Player Movement =====")]
    public float f_walkSpeed;
    [HideInInspector] public float f_moveSpeed;
    [HideInInspector] public Vector2 v_moveInput;
    [HideInInspector] public float f_mouseX;
    [HideInInspector] public float f_mouseY;
    public Transform t_playerMesh;
    public Transform t_orientation;
    public Transform t_cameraPosition;
    public bool b_canMove;
    [Space(10f)]

    [Header("===== Player Fist Combat =====")]
    public float f_punchDelay;
    public float f_softPunchTime;
    public Collider c_punchCol;

    public float f_maxInFightingTime;
    public float f_fightingCheckDis;
    public float f_targetSmoothRot;
    public float f_attackMoveForce;
    public float f_attackStamina;
    public float f_regenTime;
    float f_currentRegenTime;
    [HideInInspector] public float f_cantMoveInFightTime;
    [HideInInspector] public bool b_inFighting;
    //[HideInInspector] public bool b_lockTarget;

    [HideInInspector] public float f_currentInFightingTime;

    [HideInInspector] public bool b_canPunch;
    [HideInInspector] public float f_currentPunchDelay;
    [HideInInspector] public int i_atkCount;
    [HideInInspector] public Vector3 v_punchHitPoint;
    [Space(10f)]


    [Header("===== Player Gaurd =====")]
    //public float f_guardTime;
    public float f_guardDelay;

    [HideInInspector] public bool b_canGuard;
    [HideInInspector] public bool b_isGuard;
    [HideInInspector] public float f_currentGuardDelay;
    [HideInInspector] public float f_currentGuardTime;
    [Space(10f)]

    [Header("===== Player Interactive =====")]
    public float f_interacRange;
    public LayerMask lm_interacMask;
    public Transform t_holdObjPoint;
    /*[HideInInspector]*/
    public GameObject g_interactiveObj;
    [Space(10f)]

    [Header("===== Player Drag =====")]
    [HideInInspector] public GameObject g_dragObj;
    public Transform t_dragPos;
    public float f_dragAngle;
    [Space(10f)]

    [Header("===== Player Dead =====")]
    public Animator a_cameraAnim;
    public Animator a_fadeAnim;
    public bool b_isDead;
    int couter = 0;
    [Space(10f)]

    [Header("===== Player Sprint =====")]
    public float f_runSpeed;
    public bool b_isSprint;
    public float f_staminaMultiply;
    public float f_maxStamina;
    [HideInInspector] public float f_currentStamina;
    [Space(10f)]

    [Header("===== Area =====")]
    public AreaType currentAreaStay;

    private void Awake()
    {
        c_collider = GetComponent<CapsuleCollider>();
        c_rb = GetComponent<Rigidbody>();
        s_playerInput = GetComponent<InputSystem>();
        s_playerMovement = GetComponent<PlayerMovement>();
        s_playerFistCombat = GetComponent<FistCombat>();
        s_playerGuard = GetComponent<PlayerGuard>();
        s_playerSprint = GetComponent<PlayerSprint>();
        a_cameraAnim.enabled = false;
        f_moveSpeed = f_walkSpeed;
        i_currentHP = i_maxHP;
        c_punchCol.enabled = false;
    }

    private void Update()
    {
        if (b_inFighting)
        {
            PlayerAnimation.Instance.animator.SetBool("isFight", true);
            if (i_currentHP < i_maxHP)
            {
                f_currentRegenTime = f_regenTime;
            }
        }
        else
        {
            f_currentRegenTime -= Time.deltaTime;
            if (f_currentRegenTime <= 0)
            {
                if (i_currentHP < i_maxHP)
                {
                    CameraTrigger camTrigger = Camera.main.GetComponent<CameraTrigger>();
                    i_currentHP++;
                    camTrigger.RegenHP();
                }
                f_currentRegenTime = f_regenTime;
            }
            PlayerAnimation.Instance.animator.SetBool("isFight", false);

            if (i_currentHP == i_maxHP)
            {
                CameraTrigger camTrigger = Camera.main.GetComponent<CameraTrigger>();
                camTrigger.ResetVignetteAndFocal();
            }

        }



        Collider[] allCus = Physics.OverlapSphere(transform.position, f_fightingCheckDis, GameManager.Instance.lm_enemyMask);
        if (hasCusInFight(allCus))
        {
            f_currentInFightingTime = f_maxInFightingTime;
            b_inFighting = true;

        }
        else
        {
            b_inFighting = false;
        }

    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, f_fightingCheckDis);
    }

    bool hasCusInFight(Collider[] allCus)
    {
        if (allCus.Length > 0)
        {
            for (int i = 0; i < allCus.Length; i++)
            {
                CustomerStateManager cus = allCus[i].transform.GetComponentInParent<CustomerStateManager>();
                if (cus != null)
                {
                    if (cus.b_inFight)
                    {
                        return true;
                    }
                }
            }
        }
        return false;
    }

    public IEnumerator SkipDay()
    {
        PlayerManager.Instance.b_canMove = false;
        a_fadeAnim.SetBool("blackSkip", true);
        yield return new WaitForSeconds(1.5f);
        GameManager.Instance.MovePlayerToSpawnPoint();
        yield return new WaitForSeconds(1.5f);
        PlayerManager.Instance.b_canMove = true;
        a_fadeAnim.SetBool("blackSkip", false);
        yield return new WaitForSeconds(1f);
        if (GameManager.Instance.i_currentDay == 2)
        {
            TutorialManager.Instance.currentTutorialIndex = 14;
        }
        else if (GameManager.Instance.i_currentDay == 3)
        {
            TutorialManager.Instance.currentTutorialIndex = 38;
        }
    }

    IEnumerator DeadState()
    {
        b_isDead = true;
        b_canMove = false;
        a_cameraAnim.SetBool("dead", true);
        a_fadeAnim.SetBool("blackDead", true);

        yield return new WaitForSeconds(3f);
        a_cameraAnim.SetBool("dead", false);
        a_fadeAnim.SetBool("blackDead", false);
        yield return new WaitForSeconds(0.5f);
        CameraTrigger camTrigger = Camera.main.GetComponent<CameraTrigger>();
        camTrigger.Vignette_StepDown();
        camTrigger.FocalLength_StepDown();
        a_cameraAnim.enabled = false;
        a_cameraAnim.transform.localPosition = Vector3.zero;
        b_canMove = true;
        b_isDead = false;
        i_currentHP = i_maxHP;
        couter = 0;


        f_currentStamina = 0;

    }

    public bool TakeDamageAndDead()
    {
        i_currentHP--;

        f_currentInFightingTime = f_maxInFightingTime;

        CameraTrigger camTrigger = Camera.main.GetComponent<CameraTrigger>();
        StartCoroutine(camTrigger.Shake(camTrigger.duration, camTrigger.magnitude));
        camTrigger.Vignette_StepUp();
        camTrigger.FocalLength_StepUp();

        if (i_currentHP <= 0)
        {
            if (couter == 0)
            {
                couter++;
                a_cameraAnim.enabled = true;
                GameManager.Instance.RemoveCoin(10);
                StartCoroutine(DeadState());

                return true;
            }
            else return false;
        }
        else
        {
            return false;
        }
    }

}
