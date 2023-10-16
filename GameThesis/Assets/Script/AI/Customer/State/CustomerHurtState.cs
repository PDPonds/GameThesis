using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHurtState : BaseState
{
    public BaseState s_lastState;
    float f_fightBackPercent;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_tableObj != null)
        {
            customerStateManager.c_tableObj.b_isEmtry = true;
            customerStateManager.c_tableObj.b_readyForNextCustomer = false;
            customerStateManager.c_tableObj.s_currentCustomer = null;
        }
        customerStateManager.c_tableObj = null;
        customerStateManager.c_chairObj = null;

        f_fightBackPercent = Random.Range(0f, 100f);
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.agent.velocity = Vector3.zero;

        customerStateManager.anim.Play("Hurt");

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);

        customerStateManager.RagdollOff();

        customerStateManager.DisablePunch();

        if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                if (s_lastState == customerStateManager.s_walkAroundState)
                {
                    if (customerStateManager.b_escape)
                    {
                        if (f_fightBackPercent <= customerStateManager.f_fightBackPercent)
                        {
                            customerStateManager.SwitchState(customerStateManager.s_fightState);
                        }
                        else
                        {
                            customerStateManager.SwitchState(customerStateManager.s_giveBackState);
                        }
                    }
                }
                else if (s_lastState == customerStateManager.s_giveBackState)
                {
                    customerStateManager.SwitchState(customerStateManager.s_runEscapeState);
                }
                else
                {
                    customerStateManager.SwitchState(customerStateManager.s_fightState);
                }

            }

        }

    }
}
