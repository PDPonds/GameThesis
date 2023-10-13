using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerFrontOfCounterState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;
        customerStateManager.agent.velocity = Vector3.zero;
        customerStateManager.anim.SetBool("walk", false);
        customerStateManager.anim.SetBool("sit", false);
        customerStateManager.anim.SetBool("fightState", false);
    }

   
}
