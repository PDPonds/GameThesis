using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeHurtState : BaseState
{
    public BaseState s_lastState;

    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;
        emp.b_isWorking = false;
        emp.g_stunVFX.SetActive(false);
        emp.anim.SetBool("slackOff", false);
        emp.anim.SetBool("cooking", false);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;

        emp.agent.velocity = Vector3.zero;

        emp.anim.Play("Hurt");

        emp.RagdollOff();

        if (emp.anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            if (emp.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.5f)
            {
                if (emp.employeeType == EmployeeType.Cooking) if (!emp.b_canCook) emp.b_canCook = true;
                emp.SwitchState(emp.s_activityState);
            }
        }
    }

}
