using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHurtState : BaseState
{
    public BaseState s_lastState;
    float f_fightBackPercent;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        if (cus.c_chairObj != null)
        {
            cus.c_chairObj.DisableAllFood();
            cus.c_chairObj.b_isEmpty = true;
            cus.c_chairObj.b_readyForNextCustomer = false;
            cus.c_chairObj.s_currentCustomer = null;
        }
        cus.c_chairObj = null;

        f_fightBackPercent = Random.Range(0f, 100f);

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.agent.velocity = Vector3.zero;

        cus.anim.Play("Hurt");

        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("sit", false);
        cus.anim.SetBool("drunk", false);

        cus.RagdollOff();

        if (cus.anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            if (cus.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.2f)
            {
                if (s_lastState == cus.s_walkAroundState)
                {
                    if (cus.b_escape)
                    {
                        if (f_fightBackPercent <= cus.f_fightBackPercent)
                        {
                            cus.b_inFight = true;
                            cus.SwitchState(cus.s_fightState);
                            Retarget(cus);
                        }
                        else
                        {
                            cus.SwitchState(cus.s_giveBackState);
                        }
                    }
                    else
                    {
                        cus.b_inFight = true;
                        cus.SwitchState(cus.s_fightState);
                        Retarget(cus);


                    }
                }
                else if (s_lastState == cus.s_giveBackState)
                {
                    cus.SwitchState(cus.s_runEscapeState);
                }
                else
                {

                    cus.b_inFight = true;
                    cus.SwitchState(cus.s_fightState);
                    Retarget(cus);

                }

            }

        }
    }

    void Retarget(CustomerStateManager cus)
    {
        foreach (CustomerStateManager fighter in FightingManager.Instance.fighter)
        {
            fighter.b_fightWithPlayer = false;
        }
        cus.b_fightWithPlayer = true;
    }

}
