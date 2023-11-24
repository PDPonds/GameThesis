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
        CustomerStateManager cus = (CustomerStateManager)ai;

        cus.ApplyOutlineColor(cus.color_interact, cus.f_outlineScale);

        f_currentPayTime = cus.f_payTime;

        f_outlineTime = 0.25f;

        cus.g_sleepVFX.SetActive(false);
        cus.g_stunVFX.SetActive(false);
        cus.PauseSleepSound();
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager cus = (CustomerStateManager)ai;
        cus.agent.velocity = Vector3.zero;
        cus.anim.SetBool("walk", false);
        cus.anim.SetBool("sit", false);
        cus.anim.SetBool("drunk", false);
        cus.anim.SetBool("fightState", false);
        cus.anim.SetBool("eat", false);
        cus.anim.SetBool("checkbill", true);

        if (TutorialManager.Instance.currentTutorialIndex > 12)
        {
            f_currentPayTime -= Time.deltaTime;
            if (f_currentPayTime < 0)
            {
                cus.SwitchState(cus.s_goOutState);
            }
        }
            

        if (f_currentPayTime <= 10)
        {
            if (f_outlineTime > 0)
            {
                f_outlineTime -= Time.deltaTime;
                if(f_outlineTime <= 0)
                {
                    f_outlineTime = 0.25f;
                    if (cus.Mpb.GetFloat("_Scale") != 0)
                    {
                        Color noColor = new Color(0, 0, 0, 0);
                        cus.ApplyOutlineColor(noColor, 0f);
                    }
                    else
                    {
                        cus.ApplyOutlineColor(cus.color_interact, cus.f_outlineScale);
                    }
                }
            }
            
            

        }
    }


}
