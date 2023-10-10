using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeAttackState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.i_atkCount++;
        employeeStateManager.b_canAtk = false;
        employeeStateManager.f_currentAtkDelay = employeeStateManager.f_atkDelay;

        if (employeeStateManager.i_atkCount % 2 == 0)
        {
            employeeStateManager.anim.Play("LeftPunch");
            employeeStateManager.c_leftHandPunch.enabled = true;
        }
        else
        {
            employeeStateManager.anim.Play("RightPunch");
            employeeStateManager.c_rightHandPunch.enabled = true;
        }
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;

        employeeStateManager.agent.velocity = Vector3.zero;

        if (employeeStateManager.anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f)
        {
            employeeStateManager.SwitchState(employeeStateManager.s_fightState);
        }

    }

}
