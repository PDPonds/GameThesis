using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : Auto_Singleton<PlayerManager>
{
    public int i_HP;
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



    private void Awake()
    {
        c_collider = GetComponent<CapsuleCollider>();
        c_rb = GetComponent<Rigidbody>();
        s_playerInput = GetComponent<InputSystem>();
        s_playerMovement = GetComponent<PlayerMovement>();
        s_playerFistCombat = GetComponent<FistCombat>();
        s_playerGuard = GetComponent<PlayerGuard>();

        f_moveSpeed = f_walkSpeed;
    }

    public bool TakeDamage()
    {
        i_HP--;

        return true;
    }

}
