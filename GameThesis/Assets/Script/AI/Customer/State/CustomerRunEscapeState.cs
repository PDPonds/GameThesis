using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRunEscapeState : BaseState
{
    float f_currentRunTime;
    Vector3 v_walkPos;
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        f_currentRunTime = customerStateManager.f_runTime;

        float xPos = Random.Range(10f, 20f);
        float zPos = Random.Range(-13f, 13f);
        v_walkPos = new Vector3(xPos, 0, zPos);


        customerStateManager.img_icon.enabled = false;
        customerStateManager.img_progressBar.enabled = false;
        customerStateManager.b_escape = false;

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        f_currentRunTime -= Time.deltaTime;
        if (f_currentRunTime <= 0)
        {
            customerStateManager.SwitchState(customerStateManager.s_walkAroundState);
        }

        if (Vector3.Distance(customerStateManager.transform.position, v_walkPos) <= 0.1f)
        {
            customerStateManager.agent.velocity = Vector3.zero;
            customerStateManager.anim.SetBool("run", false);
        }
        else
        {
            customerStateManager.agent.SetDestination(v_walkPos);
            customerStateManager.anim.SetBool("run", true);

        }
        customerStateManager.agent.speed = customerStateManager.f_runSpeed;

    }
}
