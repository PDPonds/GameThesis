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
    [HideInInspector] public PlayerCrouch s_playerCrouch;
    [HideInInspector] public FistCombat s_playerFistCombat;

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

    [Header("===== Player Crouch =====")]
    public float f_standHeight;
    public float f_crouchHeight;
    [HideInInspector] public bool b_isCrouch;

    [Header("===== Player Fist Combat =====")]
    public float f_holdTimeToPunch;
    public float f_maxHoldTime;
    public float f_fistDelay;
    public Collider c_punchCol;
    public float f_stuntedTime;
    [HideInInspector] public float f_holdMoveSpeed;
    [HideInInspector] public bool b_isHold;
    [HideInInspector] public bool b_canPunch;
    [HideInInspector] public bool b_isAtk;

    [Header("===== Player Interactive =====")]
    public float f_interacRange;
    public LayerMask lm_interacMask;
    public Transform t_holdObjPoint;
    public GameObject g_interactiveObj;

    private void Awake()
    {
        c_collider = GetComponent<CapsuleCollider>();
        c_rb = GetComponent<Rigidbody>();
        s_playerInput = GetComponent<InputSystem>();
        s_playerMovement = GetComponent<PlayerMovement>();
        s_playerCrouch = GetComponent<PlayerCrouch>();
        s_playerFistCombat = GetComponent<FistCombat>();
        f_moveSpeed = f_walkSpeed;
    }

    public void TakeDamage()
    {
        i_HP--;
        b_isAtk = false;
        b_canPunch = false;
        b_isHold = false;
        s_playerFistCombat.f_holdTime = 0;

    }

}
