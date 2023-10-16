using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEscapeState : BaseState
{
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

        customerStateManager.b_escape = true;
        customerStateManager.SwitchState(customerStateManager.s_walkAroundState);

        Debug.Log("Escape");
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
    }

}
