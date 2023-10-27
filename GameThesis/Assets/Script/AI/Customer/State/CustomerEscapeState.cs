using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEscapeState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        if (customerStateManager.c_chairObj != null)
        {
            customerStateManager.c_chairObj.b_isEmpty = true;
            customerStateManager.c_chairObj.b_readyForNextCustomer = false;
            customerStateManager.c_chairObj.s_currentCustomer = null;
        }
        customerStateManager.c_chairObj = null;

        customerStateManager.b_escape = true;
        customerStateManager.SwitchState(customerStateManager.s_walkAroundState);

        customerStateManager.ApplyOutlineColor(customerStateManager.color_warning, customerStateManager.f_outlineScale);
        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.agent.speed = customerStateManager.f_walkSpeed;
        customerStateManager.RagdollOff();
    }

}
