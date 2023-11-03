using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeePassedOutState : BaseState
{
    float f_currentPressedOutTime;

    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;
        f_currentPressedOutTime = emp.f_pressedOutTime;
        emp.b_isWorking = false;

        Color noColor = new Color(0, 0, 0, 0);
        emp.ApplyOutlineColor(noColor, 0f);

        emp.g_stunVFX.SetActive(true);

        emp.anim.SetBool("slackOff", false);
        emp.anim.SetBool("cooking", false);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;
        emp.RagdollOn();

        f_currentPressedOutTime -= Time.deltaTime;
        if (f_currentPressedOutTime <= 0)
        {
            emp.SwitchState(emp.s_activityState);
        }


    }
}
