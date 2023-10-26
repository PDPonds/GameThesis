using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerCrowdState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        Color noColor = new Color(0, 0, 0, 0);
        customerStateManager.ApplyOutlineColor(noColor, 0f);
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.agent.speed = customerStateManager.f_runSpeed;
        customerStateManager.agent.SetDestination(customerStateManager.v_crowdPos);

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("walk", false);
        customerStateManager.anim.SetBool("run", true);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("drunk", false);
        customerStateManager.anim.SetBool("cheer", false);

        if (Vector3.Distance(customerStateManager.transform.position,
            customerStateManager.v_crowdPos) <= 1f)
        {
            customerStateManager.anim.SetBool("fightState", false);
            customerStateManager.anim.SetBool("walk", false);
            customerStateManager.anim.SetBool("run", false);
            customerStateManager.anim.SetBool("sit", false);
            customerStateManager.anim.SetBool("drunk", false);
            customerStateManager.anim.SetBool("cheer", true);

            customerStateManager.agent.velocity = Vector3.zero;
            customerStateManager.transform.LookAt(PlayerManager.Instance.transform.position);
        }

    }
}
