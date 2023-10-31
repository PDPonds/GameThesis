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

    [Header("===== Player Fist Combat =====")]
    public float f_punchDelay;
    public float f_softPunchTime;
    public Collider c_leftHandPunch;
    public Collider c_rightHandPunch;

    public bool b_inFighting;
    public float f_maxInFightingTime;
    [HideInInspector] public float f_currentInFightingTime;

    [HideInInspector] public bool b_canPunch;
    [HideInInspector] public float f_currentPunchDelay;
    [HideInInspector] public int i_atkCount;
    [HideInInspector] public Vector3 v_punchHitPoint;


    [Header("===== Player Gaurd =====")]
    public float f_guardTime;
    public float f_guardDelay;

    [HideInInspector] public bool b_canGuard;
    [HideInInspector] public bool b_isGuard;
    [HideInInspector] public float f_currentGuardDelay;
    [HideInInspector] public float f_currentGuardTime;

    [Header("===== Player Interactive =====")]
    public float f_interacRange;
    public LayerMask lm_interacMask;
    public Transform t_holdObjPoint;
    [HideInInspector] public GameObject g_interactiveObj;

    [Header("===== Player Drag =====")]
    [HideInInspector] public GameObject g_dragObj;
    public Transform t_dragPos;
    public float f_dragAngle;

    [Header("===== Player Dead =====")]
    public Animator a_cameraAnim;
    public Animator a_fadeAnim;
    public bool b_isDead;
    int couter = 0;

    [Header("===== Player Sprint =====")]
    public float f_runSpeed;
    public bool b_isSprint;
    public float f_staminaMultiply;
    public float f_maxStamina;
    [HideInInspector] public float f_currentStamina;

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
    }

    private void Update()
    {
        if (b_inFighting)
        {
            PlayerAnimation.Instance.animator.SetBool("isFight", true);
            f_currentInFightingTime -= Time.deltaTime;
            if (f_currentInFightingTime < 0)
            {
                i_currentHP = i_maxHP;
                b_inFighting = false;

                CameraTrigger camTrigger = Camera.main.GetComponent<CameraTrigger>();
                camTrigger.ResetVignetteAndFocal();
            }
        }
        else
        {
            PlayerAnimation.Instance.animator.SetBool("isFight", false);
        }

    }

    IEnumerator DeadState()
    {
        b_isDead = true;
        b_canMove = false;
        a_cameraAnim.SetBool("dead", true);
        a_fadeAnim.SetBool("black", true);

        for (int i = 0; i < RestaurantManager.Instance.allSheriffs.Length; i++)
        {
            SheriffStateManager shrSM = RestaurantManager.Instance.allSheriffs[i];
            if (shrSM.s_currentState == shrSM.s_waitForFightEnd)
            {
                shrSM.SwitchState(shrSM.s_activityState);
            }
        }

        for (int i = 0; i < RestaurantManager.Instance.allCustomers.Length; i++)
        {
            CustomerStateManager cus = RestaurantManager.Instance.allCustomers[i];
            if (cus.b_inFight)
            {
                cus.b_inFight = false;
                cus.SwitchState(cus.s_walkAroundState);
            }
        }

        yield return new WaitForSeconds(3f);
        a_cameraAnim.SetBool("dead", false);
        a_fadeAnim.SetBool("black", false);
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

        b_inFighting = true;
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
