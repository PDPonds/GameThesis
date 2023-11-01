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
        employeeStateManager.g_stunVFX.SetActive(false);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.agent.velocity = Vector3.zero;

        employeeStateManager.anim.Play("Hurt");

        employeeStateManager.RagdollOff();

        if (employeeStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            if(employeeStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                employeeStateManager.SwitchState(employeeStateManager.s_activityState);
            }
        }
    }

}
