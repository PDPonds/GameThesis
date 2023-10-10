using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeHurtState : BaseState
{
    public BaseState s_lastState;

    public override void EnterState(StateManager ai)
    {
        
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.agent.velocity = Vector3.zero;

        employeeStateManager.anim.Play("Hurt");

        employeeStateManager.RagdollOff();

        employeeStateManager.DisablePunch();

        if (!employeeStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            if(s_lastState != employeeStateManager.s_passedOutState)
            {
                employeeStateManager.SwitchState(employeeStateManager.s_fightState);
            }
            else
            {
                employeeStateManager.SwitchState(employeeStateManager.s_activityState);

            }
        }

    }

}
