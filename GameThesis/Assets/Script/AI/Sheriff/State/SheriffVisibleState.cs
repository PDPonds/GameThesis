using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SheriffVisibleState : BaseState
{
    float f_currentVisibleTime;
    public override void EnterState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
        sheriffStateManager.g_sheriffMesh.SetActive(false);
        f_currentVisibleTime = sheriffStateManager.f_visibleTime;
    }

    public override void UpdateState(StateManager ai)
    {
        SheriffStateManager sheriffStateManager = (SheriffStateManager)ai;
        sheriffStateManager.agent.velocity = Vector3.zero;
        sheriffStateManager.anim.SetBool("walk", false);
        sheriffStateManager.anim.SetBool("run", false);

        f_currentVisibleTime -= Time.deltaTime;
        if (f_currentVisibleTime <= 0)
        {
            sheriffStateManager.SwitchState(sheriffStateManager.s_activityState);
        }
    }
}
