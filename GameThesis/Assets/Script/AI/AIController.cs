using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour
{
    public int i_HP;
    public AIType type;
    public AIState state = AIState.Activity;
    Rigidbody[] rb;
    Animator anim;

    public float f_atkDelay;
    bool b_atk;
    float f_currentAtk;
    int i_atkCount;

    private void Awake()
    {
        anim = GetComponent<Animator>();
        rb = GetComponentsInChildren<Rigidbody>();
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

                        Collider[] player = Physics.OverlapSphere(transform.position, 1f, GameManager.Instance.lm_playerMask);
                        if (player.Length > 0)
                        {
                            if (b_atk)
                            {
                                if (i_atkCount % 2 == 0) anim.Play("LeftPunch");
                                else anim.Play("RightPunch");
                                f_currentAtk = f_atkDelay;
                                b_atk = false;
                                f_currentAtk = f_atkDelay;
                            }
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
    }

    public void TakeDamege()
    {
        i_HP--;
    }

    void RagdollOn()
    {
        anim.enabled = false;
        foreach (Rigidbody rb in rb) { rb.isKinematic = false; }
    }

    void RagdollOff()
    {
        anim.enabled = true;
        foreach (Rigidbody rb in rb) { rb.isKinematic = true; }
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