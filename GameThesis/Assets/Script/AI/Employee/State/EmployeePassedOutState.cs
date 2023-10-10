using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeePassedOutState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.RagdollOn();
        employeeStateManager.DisablePunch();
    }
}
