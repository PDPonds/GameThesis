using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Animations.Rigging;

public class AIController : MonoBehaviour, IInteracable
{
    [Header("===== AIDetail =====")]
    public int i_HP;
    public AIType type;
    public AIState state = AIState.Activity;
    Rigidbody[] rb;
    Animator anim;
    NavMeshAgent nav;

    [Header("===== AI Atk =====")]
    public float f_atkDelay;
    public float f_atkRange;

    bool b_atk;
    float f_currentAtk;
    int i_atkCount;

    [SerializeField] bool b_dragThis;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentsInChildren<Rigidbody>();
        nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        switch (type)
        {
            case AIType.Customer:

                switch (state)
                {
                    case AIState.Activity:

                        RagdollOff();
                        anim.SetBool("fightState", false);

                        break;
                    case AIState.Fight:

                        RagdollOff();
                        anim.SetBool("fightState", true);

                        Collider[] player = Physics.OverlapSphere(transform.position, f_atkRange, GameManager.Instance.lm_playerMask);
                        if (player.Length > 0)
                        {
                            nav.velocity = Vector3.zero;
                            if (b_atk)
                            {
                                nav.velocity = Vector3.zero;
                                if (i_atkCount % 2 == 0) anim.Play("LeftPunch");
                                else anim.Play("RightPunch");
                                i_atkCount++;
                                f_currentAtk = f_atkDelay;
                                b_atk = false;
                                f_currentAtk = f_atkDelay;
                            }
                        }
                        else
                        {
                            nav.SetDestination(PlayerManager.Instance.transform.position);
                        }

                        if (!b_atk)
                        {
                            f_currentAtk -= Time.deltaTime;
                            if (f_currentAtk <= 0)
                            {
                                b_atk = true;
                            }
                        }

                        break;
                    case AIState.Dead:

                        RagdollOn();

                        if (b_dragThis)
                        {
                            transform.position = PlayerManager.Instance.t_holdObjPoint.position;
                        }

                        break;
                    default: break;
                }

                break;
            case AIType.Employee:

                switch (state)
                {
                    case AIState.Activity:

                        RagdollOff();
                        anim.SetBool("fightState", false);

                        break;
                    case AIState.Fight:

                        RagdollOff();
                        anim.SetBool("fightState", true);
                        break;
                    case AIState.Dead:

                        RagdollOn();

                        break;
                    default: break;
                }

                break;
            case AIType.Animal:

                switch (state)
                {
                    case AIState.Activity:

                        RagdollOff();

                        break;
                    case AIState.Escape:

                        RagdollOff();

                        break;
                    case AIState.Dead:

                        RagdollOn();

                        break;
                    default: break;
                }

                break;
            default: break;
        }

        if (i_HP <= 0)
        {
            state = AIState.Dead;
        }

    }

    public void Interaction()
    {
        if (state == AIState.Dead)
        {
            if (PlayerManager.Instance.t_holdObjPoint.childCount < 1)
            {
                b_dragThis = !b_dragThis;
            }

            PlayerManager.Instance.g_interactiveObj = null;
        }
    }

    public void TakeDamege()
    {
        i_HP--;
    }

    void RagdollOn()
    {
        anim.enabled = false;
        nav.enabled = false;
        foreach (Rigidbody rb in rb) { rb.isKinematic = false; }
    }

    void RagdollOff()
    {
        anim.enabled = true;
        nav.enabled = true;
        foreach (Rigidbody rb in rb) { rb.isKinematic = true; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, f_atkRange);
    }

}

public enum AIState
{
    Activity, Escape, Fight, Dead
}

public enum AIType
{
    Customer, Employee, Animal
}