using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPushState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_tableObj != null)
        {
            customerStateManager.c_tableObj.b_isEmpty = true;
            customerStateManager.c_tableObj.b_readyForNextCustomer = false;
            customerStateManager.c_tableObj.s_currentCustomer = null;
        }
        customerStateManager.c_tableObj = null;
        customerStateManager.c_chairObj = null;
        customerStateManager.anim.Play("Push");
    }

    public override void UpdateState(StateManager ai)
    {

        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.agent.velocity = Vector3.zero;

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);

        customerStateManager.RagdollOff();

        customerStateManager.DisablePunch();

        if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Push"))
        {
            if (customerStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                customerStateManager.SwitchState(customerStateManager.s_fightState);
            }
        }

    }
}
