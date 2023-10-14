using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeePassedOutState : BaseState
{
    float f_currentPressedOutTime;

    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        f_currentPressedOutTime = employeeStateManager.f_pressedOutTime;
        employeeStateManager.b_isWorking = false;
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.RagdollOn();
        employeeStateManager.DisablePunch();
        
        f_currentPressedOutTime -= Time.deltaTime;
        if(f_currentPressedOutTime <= 0)
        {
            employeeStateManager.SwitchState(employeeStateManager.s_activityState);
        }
    }
}
