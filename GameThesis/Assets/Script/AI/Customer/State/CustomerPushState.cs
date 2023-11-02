using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerPushState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        if (cus.c_chairObj != null)
        {
            cus.c_chairObj.b_isEmpty = true;
            cus.c_chairObj.b_readyForNextCustomer = false;
            cus.c_chairObj.s_currentCustomer = null;
            cus.c_chairObj.DisableAllFood();

        }
        cus.c_chairObj = null;
        cus.anim.Play("Push");
        cus.ApplyOutlineColor(cus.color_warning, cus.f_outlineScale);

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {

        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.agent.velocity = Vector3.zero;

        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("sit", false);
        cus.anim.SetBool("drunk", false);

        cus.RagdollOff();

        if (cus.anim.GetCurrentAnimatorStateInfo(0).IsName("Push"))
        {
            if (cus.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                cus.SwitchState(cus.s_fightState);
            }
        }

    }
}
