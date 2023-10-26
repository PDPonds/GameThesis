using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class SheriffStateManager : StateManager
{
    public override BaseState s_currentState { get; set; }

    public SheriffActivityState s_activityState = new SheriffActivityState();
    public SheriffVisibleState s_visibleState = new SheriffVisibleState();
    public SheriffActiveCrowdState s_activeThrong = new SheriffActiveCrowdState();
    public SheriffWaitForFightEndState s_waitForFightEnd = new SheriffWaitForFightEndState();

    //[HideInInspector] public Rigidbody[] rb;
    [HideInInspector] public Animator anim;
    [HideInInspector] public NavMeshAgent agent;

    [Header("===== Activity =====")]
    public GameObject g_sheriffMesh;
    public float f_walkSpeed;
    public float f_runSpeed;
    public float f_visibleTime;
    public float f_cost;
    [HideInInspector] public int i_walkCount;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        //rb = GetComponentsInChildren<Rigidbody>();

    }

    private void Start()
    {
        SwitchState(s_activityState);

    }

    private void Update()
    {
        s_currentState.UpdateState(this);
    }

}
