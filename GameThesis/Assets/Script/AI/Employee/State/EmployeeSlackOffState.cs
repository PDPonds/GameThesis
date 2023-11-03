using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EmployeeSlackOffState : BaseState
{
    float f_currentSlowTime;
    public override void EnterState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;
        emp.b_isWorking = false;

        float xPos = Random.Range(emp.v_minmaxX.x, emp.v_minmaxX.y);
        float zPos = Random.Range(emp.v_minmaxZ.x, emp.v_minmaxZ.y);
        emp.v_walkPos = new Vector3(xPos, 0, zPos);

        emp.f_slackOffTime = Random.Range(emp.v_minAndMaxSlackOffTime.x,
            emp.v_minAndMaxSlackOffTime.y);
        f_currentSlowTime = emp.f_slackOffTime;

        if (emp.employeeType == EmployeeType.Serve)
        {
            emp.b_hasFood = false;
        }

        emp.ApplyOutlineColor(emp.color_warning, emp.f_outlineScale);
        emp.g_stunVFX.SetActive(false);

        int slackOffNum = Random.Range(0, emp.allSlackOffAnim.Count);
        emp.anim.runtimeAnimatorController = emp.allSlackOffAnim[slackOffNum];

        emp.anim.SetBool("cooking", false);

    }

    public override void UpdateState(StateManager ai)
    {
        EmployeeStateManager emp = (EmployeeStateManager)ai;
        emp.RagdollOff();

        emp.agent.SetDestination(emp.v_walkPos);
        if (Vector3.Distance(emp.transform.position, emp.v_walkPos)
            <= 1f)
        {
            emp.anim.SetBool("walk", false);
            emp.anim.SetBool("slackOff", true);
            f_currentSlowTime -= Time.deltaTime;
            if (f_currentSlowTime < 0)
            {
                emp.SwitchState(emp.s_activityState);
            }

        }
        else
        {
            emp.anim.SetBool("walk", true);
            emp.anim.SetBool("slackOff", false);
        }

        emp.agent.speed = emp.f_walkSpeed;

    }
}
