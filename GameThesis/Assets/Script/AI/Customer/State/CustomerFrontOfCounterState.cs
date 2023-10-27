using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CustomerFrontOfCounterState : BaseState
{
    float f_currentPayTime;

    float f_outlineTime;

    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.ApplyOutlineColor(customerStateManager.color_interact, customerStateManager.f_outlineScale);

        f_currentPayTime = customerStateManager.f_payTime;

        f_outlineTime = 0.25f;

        customerStateManager.g_sleepVFX.SetActive(false);
        customerStateManager.g_stunVFX.SetActive(false);

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

        if (f_currentPayTime <= 5)
        {
            if (f_outlineTime > 0)
            {
                f_outlineTime -= Time.deltaTime;
                if(f_outlineTime <= 0)
                {
                    f_outlineTime = 0.25f;
                    if (customerStateManager.Mpb.GetFloat("_Scale") != 0)
                    {
                        Color noColor = new Color(0, 0, 0, 0);
                        customerStateManager.ApplyOutlineColor(noColor, 0f);
                    }
                    else
                    {
                        customerStateManager.ApplyOutlineColor(customerStateManager.color_interact, customerStateManager.f_outlineScale);
                    }
                }
            }
            
            

        }
    }


}
