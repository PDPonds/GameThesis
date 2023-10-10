using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class EmployeeServeAndCookingState : BaseState
{
   
    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.i_currentHP = employeeStateManager.i_maxHP;
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        switch(employeeStateManager.employeeType)
        {
            case EmployeeType.Cooking:

            break;
            case EmployeeType.Serve:

            break;
            default: break;
        }

        employeeStateManager.anim.SetBool("fightState", false);

        employeeStateManager.DisablePunch();
        employeeStateManager.RagdollOff();

        if (!employeeStateManager.b_canAtk)
        {
            employeeStateManager.f_currentAtkDelay -= Time.deltaTime;
            if (employeeStateManager.f_currentAtkDelay <= 0)
            {
                employeeStateManager.b_canAtk = true;
            }
        }
    }
    
}