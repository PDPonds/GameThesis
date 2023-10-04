using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerHurtState : BaseState
{
    public override void EnterState(StateManager ai)
    {

    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.agent.velocity = Vector3.zero;

        customerStateManager.anim.Play("Hurt");

        customerStateManager.RagdollOff();

        customerStateManager.DisablePunch();

        if (!customerStateManager.anim.GetCurrentAnimatorStateInfo(0).IsName("Hurt"))
        {
            customerStateManager.SwitchState(customerStateManager.s_fightState);
        }

    }
}
