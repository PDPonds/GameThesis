using System.Collections;
using System.Collections.Generic;
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
    public float f_heavyPunchTime;
    public float f_softPunchTime;
    public Collider c_leftHandPunch;
    public Collider c_rightHandPunch;
    [HideInInspector] public bool b_canPunch;
    [HideInInspector] public bool b_isHold;
    [HideInInspector] public float f_currentHoldTime;
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
    public GameObject g_interactiveObj;

    [Header("===== Player Drag =====")]
    public GameObject g_dragObj;
    public Transform t_dragPos;

    [Header("===== Player Dead =====")]
    public Animator a_cameraAnim;
    public Animator a_fadeAnim;
    public bool b_isDead;
    private void Awake()
    {
        c_collider = GetComponent<CapsuleCollider>();
        c_rb = GetComponent<Rigidbody>();
        s_playerInput = GetComponent<InputSystem>();
        s_playerMovement = GetComponent<PlayerMovement>();
        s_playerFistCombat = GetComponent<FistCombat>();
        s_playerGuard = GetComponent<PlayerGuard>();
        a_cameraAnim.enabled = false;
        f_moveSpeed = f_walkSpeed;
        i_currentHP = i_maxHP;
    }


    IEnumerator DeadState()
    {
        b_isDead = true;
        b_canMove = false;
        a_cameraAnim.SetBool("dead", true);
        a_fadeAnim.SetBool("black", true);
        yield return new WaitForSeconds(3f);
        a_cameraAnim.SetBool("dead", false);
        a_fadeAnim.SetBool("black", false);
        yield return new WaitForSeconds(0.5f);
        a_cameraAnim.enabled = false;
        b_canMove = true;
        b_isDead = false;
        i_currentHP = i_maxHP;
    }

    public bool TakeDamageAndDead()
    {
        i_currentHP--;
        if (i_currentHP <= 0)
        {
            StartCoroutine(DeadState());
            a_cameraAnim.enabled = true;
            return true;
        }

        return false;
    }

}
