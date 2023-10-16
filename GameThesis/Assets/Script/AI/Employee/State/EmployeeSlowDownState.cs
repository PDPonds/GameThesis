using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeSlowDownState : BaseState
{
    float f_currentSlowTime;
    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.b_isWorking = false;

        float xPos = Random.Range(employeeStateManager.v_minmaxX.x, employeeStateManager.v_minmaxX.y);
        float zPos = Random.Range(employeeStateManager.v_minmaxZ.x, employeeStateManager.v_minmaxZ.y);
        employeeStateManager.v_walkPos = new Vector3(xPos, 0, zPos);

        employeeStateManager.f_slowTime = Random.Range(employeeStateManager.v_minAndMaxSlowTime.x,
            employeeStateManager.v_minAndMaxSlowTime.y);
        f_currentSlowTime = employeeStateManager.f_slowTime;

        Debug.Log("SlowDown");
    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager employeeStateManager = (EmployeeStateManager)ai;
        employeeStateManager.RagdollOff();

        employeeStateManager.agent.SetDestination(employeeStateManager.v_walkPos);
        if (Vector3.Distance(employeeStateManager.transform.position, employeeStateManager.v_walkPos)
            <= 0.1f)
        {
            employeeStateManager.anim.SetBool("walk", false);
            f_currentSlowTime -= Time.deltaTime;
            if(f_currentSlowTime < 0)
            {
                employeeStateManager.SwitchState(employeeStateManager.s_activityState);
            }

        }
        else
        {
            employeeStateManager.anim.SetBool("walk", true);
        }

        employeeStateManager.agent.speed = employeeStateManager.f_walkSpeed;

    }
}
