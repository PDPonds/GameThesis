using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerRunOutState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        
    }

    public override void UpdateState(StateManager ai)
    {
        CustomerStateManager customerStateManager = (CustomerStateManager)ai;

        customerStateManager.anim.SetBool("fightState", false);
        customerStateManager.anim.SetBool("sit", false);
    }

}
