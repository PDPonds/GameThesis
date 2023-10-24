using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerFrontOfCounterState : BaseState
{
    float f_currentPayTime;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.ApplyOutlineColor(customerStateManager.color_interact, customerStateManager.f_outlineScale);

        f_currentPayTime = customerStateManager.f_payTime;

        customerStateManager.img_wakeUpImage.enabled = false;
        customerStateManager.img_BGWakeUpImage.enabled = false;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.agent.velocity = Vector3.zero;
        customerStateManager.anim.SetBool("walk", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.anim.SetBool("fightState", false);

        f_currentPayTime -= Time.deltaTime;
        if (f_currentPayTime < 0)
        {
            customerStateManager.SwitchState(customerStateManager.s_goOutState);
        }

    }


}
