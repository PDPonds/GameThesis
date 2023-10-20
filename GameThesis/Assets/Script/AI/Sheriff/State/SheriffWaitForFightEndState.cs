using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffWaitForFightEndState : BaseState
{
    public override void EnterState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
    }

    public override void UpdateState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
        sheriffStateManager.agent.velocity = Vector3.zero;
        sheriffStateManager.anim.SetBool("walk", false);
        sheriffStateManager.anim.SetBool("run", false);
    }
}
