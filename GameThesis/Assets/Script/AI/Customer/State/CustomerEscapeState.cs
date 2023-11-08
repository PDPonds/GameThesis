using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerEscapeState : BaseState
{
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

        cus.b_escape = true;
        cus.SwitchState(cus.s_walkAroundState);

        cus.ApplyOutlineColor(cus.color_warning, cus.f_outlineScale);
        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.agent.speed = cus.f_walkSpeed;
        cus.RagdollOff();
    }

}
