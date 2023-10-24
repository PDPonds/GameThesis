using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeHurtState : BaseState
{
    public BaseState s_lastState;

    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.b_isWorking = false;
        employeeStateManager.ApplyOutlineColor(employeeStateManager.color_punch, employeeStateManager.f_outlineScale);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.agent.velocity = Vector3.zero;

        employeeStateManager.anim.Play("Hurt");

        employeeStateManager.RagdollOff();

        employeeStateManager.DisablePunch();

        if (employeeStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            if(employeeStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                if(s_lastState == employeeStateManager.s_passedOutState)
                {
                    employeeStateManager.SwitchState(employeeStateManager.s_activityState);
                }
                else if(s_lastState == employeeStateManager.s_slowDownState)
                {
                    employeeStateManager.SwitchState(employeeStateManager.s_activityState);
                }
                else
                {
                    employeeStateManager.SwitchState(employeeStateManager.s_fightState);
                }
            }
        }
    }

}
